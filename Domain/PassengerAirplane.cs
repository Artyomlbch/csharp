using AircraftIndustry.Components;

namespace AircraftIndustry.Domain;

// пассажирский самолёт, предназначенный для перевозки людей
public class PassengerAirplane : Airplane
{
    // базовая ставка стоимости лётного часа, у.е
    private const decimal BaseHourRate = 4500m;

    // надбавка за каждое пассажирское место, у.е
    private const decimal RatePerSeat = 12m;

    // количество пассажирских мест
    public int SeatCount { get; }

    // наличие салона бизнес-класса
    public bool HasBusinessClass { get; }

    // создаёт пассажирский самолёт
    public PassengerAirplane(
        string registrationNumber,
        string model,
        Manufacturer manufacturer,
        Engine engine,
        double maxTakeoffWeightKg,
        double maxSpeedKmH,
        double maxRangeKm,
        double wingspanM,
        int engineCount,
        double requiredRunwayLengthM,
        int seatCount,
        bool hasBusinessClass)
        : base(registrationNumber, model, manufacturer, engine, maxTakeoffWeightKg,
               maxSpeedKmH, maxRangeKm, wingspanM, engineCount, requiredRunwayLengthM)
    {
        SeatCount = seatCount;
        HasBusinessClass = hasBusinessClass;
    }

    // рассчитывает стоимость лётного часа исходя из вместимости салона
    // наличие бизнес-класса увеличивает стоимость обслуживания на 15%
    public override decimal CalculateFlightHourCost()
    {
        decimal cost = BaseHourRate + SeatCount * RatePerSeat;

        if (HasBusinessClass)
        {
            cost *= 1.15m;
        }

        return decimal.Round(cost, 2);
    }

    // возвращает описание назначения пассажирского самолёта
    public override string DescribePurpose() =>
        $"Пассажирский самолёт на {SeatCount} мест" +
        (HasBusinessClass ? " с салоном бизнес-класса." : ".");

    // рассчитывает выручку рейса при заданной загрузке и цене билета
    public decimal CalculateRevenue(double loadFactor, decimal ticketPrice)
    {
        if (loadFactor is < 0 or > 1)
        {
            throw new ArgumentOutOfRangeException(
                nameof(loadFactor), "Коэффициент загрузки должен быть в диапазоне от 0 до 1.");
        }

        int occupiedSeats = (int)(SeatCount * loadFactor);
        return occupiedSeats * ticketPrice;
    }
}