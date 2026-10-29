using AircraftIndustry.Components;
using AircraftIndustry.Domain;

namespace AircraftIndustry.Storage;

// Ангар — место хранения и обслуживания парка летательных аппаратов.
// Обеспечивает регистрацию техники, поиск и подбор машин под заявки.
public class Hangar
{
    // Парк летательных аппаратов. Изменяется только методами класса.
    private readonly List<Aircraft> _fleet = new();

    // Название ангара.
    public string Name { get; }

    // Аэродром базирования.
    public string Location { get; }

    // Вместимость ангара, машино-мест.
    public int Capacity { get; }

    // Количество размещённых аппаратов.
    public int OccupiedPlaces => _fleet.Count;

    // Количество свободных машино-мест.
    public int FreePlaces => Capacity - _fleet.Count;

    // Парк аппаратов, доступный только для чтения.
    public IReadOnlyList<Aircraft> Fleet => _fleet;

    // Создаёт ангар с заданными параметрами.
    public Hangar(string name, string location, int capacity)
    {
        if (capacity <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(capacity), "Вместимость ангара должна быть положительной.");
        }

        Name = name;
        Location = location;
        Capacity = capacity;
    }

    // Регистрирует летательный аппарат в ангаре.
    public bool Register(Aircraft aircraft)
    {
        if (FreePlaces == 0)
        {
            Console.WriteLine($"Ангар «{Name}» заполнен, размещение невозможно.");
            return false;
        }

        bool alreadyRegistered = _fleet.Any(
            a => a.RegistrationNumber == aircraft.RegistrationNumber);

        if (alreadyRegistered)
        {
            Console.WriteLine(
                $"Аппарат с номером {aircraft.RegistrationNumber} уже зарегистрирован.");
            return false;
        }

        _fleet.Add(aircraft);
        return true;
    }

    // Снимает аппарат с регистрации по бортовому номеру.
    public bool Remove(string registrationNumber)
    {
        Aircraft? aircraft = FindByRegistration(registrationNumber);

        if (aircraft is null)
        {
            return false;
        }

        _fleet.Remove(aircraft);
        return true;
    }

    // Ищет аппарат по бортовому номеру.
    public Aircraft? FindByRegistration(string registrationNumber) =>
        _fleet.FirstOrDefault(a => a.RegistrationNumber == registrationNumber);

    // Возвращает все аппараты заданного производителя.
    public List<Aircraft> FindByManufacturer(string manufacturerName) =>
        _fleet.Where(a => a.Manufacturer.Name == manufacturerName).ToList();

    // Возвращает все аппараты, требующие технического обслуживания.
    public List<Aircraft> GetAircraftNeedingMaintenance() =>
        _fleet.Where(a => a.NeedsMaintenance).ToList();

    // Подбирает аппараты, способные выполнить заданную заявку на рейс.
    public List<Aircraft> FindSuitableAircraft(FlightRequest request)
    {
        List<Aircraft> suitable = new();

        foreach (Aircraft aircraft in _fleet)
        {
            if (!aircraft.CanCoverDistance(request.DistanceKm))
            {
                continue;
            }

            if (aircraft is Airplane airplane &&
                !airplane.CanUseRunway(request.AvailableRunwayLengthM))
            {
                continue;
            }

            if (request.RequiredSeats > 0)
            {
                if (aircraft is not PassengerAirplane passenger ||
                    passenger.SeatCount < request.RequiredSeats)
                {
                    continue;
                }
            }

            if (request.RequiredPayloadKg > 0 && !CanCarryPayload(aircraft, request.RequiredPayloadKg))
            {
                continue;
            }

            suitable.Add(aircraft);
        }

        return suitable;
    }

    // Определяет, способен ли аппарат перевезти груз заданной массы.
    private static bool CanCarryPayload(Aircraft aircraft, double payloadKg) => aircraft switch
    {
        CargoAirplane cargo => cargo.MaxPayloadKg >= payloadKg,
        TransportHelicopter transport =>
            transport.MaxPayloadKg >= payloadKg || transport.ExternalSlingCapacityKg >= payloadKg,
        _ => false
    };

    // Рассчитывает суммарную стоимость лётного часа всего парка.
    public decimal CalculateTotalFleetHourCost() =>
        _fleet.Sum(a => a.CalculateFlightHourCost());

    // Возвращает аппарат с наибольшей дальностью полёта.
    public Aircraft? GetLongestRangeAircraft() =>
        _fleet.OrderByDescending(a => a.MaxRangeKm).FirstOrDefault();

    // Группирует парк по категориям техники.
    public Dictionary<string, int> GetFleetStatistics() =>
        _fleet.GroupBy(a => a.Category)
              .ToDictionary(group => group.Key, group => group.Count());

    // Возвращает строковое представление ангара.
    public override string ToString() =>
        $"Ангар «{Name}» ({Location}): занято {OccupiedPlaces} из {Capacity} мест";
}