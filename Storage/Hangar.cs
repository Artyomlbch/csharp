using AircraftIndustry.Components;
using AircraftIndustry.Domain;

namespace AircraftIndustry.Storage;

/// <summary>
/// Ангар — место хранения и обслуживания парка летательных аппаратов.
/// Обеспечивает регистрацию техники, поиск и подбор машин под заявки.
/// </summary>
public class Hangar
{
    /// <summary>Парк летательных аппаратов. Изменяется только методами класса.</summary>
    private readonly List<Aircraft> _fleet = new();

    /// <summary>Название ангара.</summary>
    public string Name { get; }

    /// <summary>Аэродром базирования.</summary>
    public string Location { get; }

    /// <summary>Вместимость ангара, машино-мест.</summary>
    public int Capacity { get; }

    /// <summary>Количество размещённых аппаратов.</summary>
    public int OccupiedPlaces => _fleet.Count;

    /// <summary>Количество свободных машино-мест.</summary>
    public int FreePlaces => Capacity - _fleet.Count;

    /// <summary>Парк аппаратов, доступный только для чтения.</summary>
    public IReadOnlyList<Aircraft> Fleet => _fleet;

    /// <summary>
    /// Создаёт ангар с заданными параметрами.
    /// </summary>
    /// <param name="name">Название ангара.</param>
    /// <param name="location">Аэродром базирования.</param>
    /// <param name="capacity">Вместимость, машино-мест.</param>
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

    /// <summary>
    /// Регистрирует летательный аппарат в ангаре.
    /// </summary>
    /// <param name="aircraft">Размещаемый аппарат.</param>
    /// <returns>true, если аппарат успешно размещён.</returns>
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

    /// <summary>
    /// Снимает аппарат с регистрации по бортовому номеру.
    /// </summary>
    /// <param name="registrationNumber">Бортовой номер.</param>
    /// <returns>true, если аппарат был найден и удалён.</returns>
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

    /// <summary>
    /// Ищет аппарат по бортовому номеру.
    /// </summary>
    /// <param name="registrationNumber">Бортовой номер.</param>
    /// <returns>Найденный аппарат или null.</returns>
    public Aircraft? FindByRegistration(string registrationNumber) =>
        _fleet.FirstOrDefault(a => a.RegistrationNumber == registrationNumber);

    /// <summary>
    /// Возвращает все аппараты заданного производителя.
    /// </summary>
    /// <param name="manufacturerName">Название производителя.</param>
    /// <returns>Список подходящих аппаратов.</returns>
    public List<Aircraft> FindByManufacturer(string manufacturerName) =>
        _fleet.Where(a => a.Manufacturer.Name == manufacturerName).ToList();

    /// <summary>
    /// Возвращает все аппараты, требующие технического обслуживания.
    /// </summary>
    /// <returns>Список аппаратов с превышенной наработкой.</returns>
    public List<Aircraft> GetAircraftNeedingMaintenance() =>
        _fleet.Where(a => a.NeedsMaintenance).ToList();

    /// <summary>
    /// Подбирает аппараты, способные выполнить заданную заявку на рейс.
    /// </summary>
    /// <param name="request">Заявка на рейс.</param>
    /// <returns>Список подходящих аппаратов.</returns>
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

    /// <summary>
    /// Определяет, способен ли аппарат перевезти груз заданной массы.
    /// </summary>
    /// <param name="aircraft">Проверяемый аппарат.</param>
    /// <param name="payloadKg">Масса груза, кг.</param>
    /// <returns>true, если аппарат подходит для перевозки.</returns>
    private static bool CanCarryPayload(Aircraft aircraft, double payloadKg) => aircraft switch
    {
        CargoAirplane cargo => cargo.MaxPayloadKg >= payloadKg,
        TransportHelicopter transport =>
            transport.MaxPayloadKg >= payloadKg || transport.ExternalSlingCapacityKg >= payloadKg,
        _ => false
    };

    /// <summary>
    /// Рассчитывает суммарную стоимость лётного часа всего парка.
    /// </summary>
    /// <returns>Сумма стоимостей лётного часа всех аппаратов.</returns>
    public decimal CalculateTotalFleetHourCost() =>
        _fleet.Sum(a => a.CalculateFlightHourCost());

    /// <summary>
    /// Возвращает аппарат с наибольшей дальностью полёта.
    /// </summary>
    /// <returns>Аппарат с максимальной дальностью или null, если парк пуст.</returns>
    public Aircraft? GetLongestRangeAircraft() =>
        _fleet.OrderByDescending(a => a.MaxRangeKm).FirstOrDefault();

    /// <summary>
    /// Группирует парк по категориям техники.
    /// </summary>
    /// <returns>Словарь: категория — количество аппаратов.</returns>
    public Dictionary<string, int> GetFleetStatistics() =>
        _fleet.GroupBy(a => a.Category)
              .ToDictionary(group => group.Key, group => group.Count());

    /// <summary>
    /// Возвращает строковое представление ангара.
    /// </summary>
    public override string ToString() =>
        $"Ангар «{Name}» ({Location}): занято {OccupiedPlaces} из {Capacity} мест";
}