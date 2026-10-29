using AircraftIndustry.Components;

namespace AircraftIndustry.Domain;

// Пассажирский самолёт, предназначенный для перевозки людей.
public class PassengerAirplane : Airplane
{
    // Базовая ставка стоимости лётного часа, у.е.
    private const decimal BaseHourRate = 4500m;

    // Надбавка за каждое пассажирское место, у.е.
    private const decimal RatePerSeat = 12m;

    // Количество пассажирских мест.
    public int SeatCount { get; }

    // Наличие салона бизнес-класса.
    public bool HasBusinessClass { get; }

    // Создаёт пассажирский самолёт.
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

    // Рассчитывает стоимость лётного часа исходя из вместимости салона.
    // Наличие бизнес-класса увеличивает стоимость обслуживания на 15%.
    public override decimal CalculateFlightHourCost()
    {
        decimal cost = BaseHourRate + SeatCount * RatePerSeat;

        if (HasBusinessClass)
        {
            cost *= 1.15m;
        }

        return decimal.Round(cost, 2);
    }

    // Возвращает описание назначения пассажирского самолёта.
    public override string DescribePurpose() =>
        $"Пассажирский самолёт на {SeatCount} мест" +
        (HasBusinessClass ? " с салоном бизнес-класса." : ".");

    // Рассчитывает выручку рейса при заданной загрузке и цене билета.
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