using AircraftIndustry.Components;

namespace AircraftIndustry.Domain;

/// <summary>
/// Пассажирский самолёт, предназначенный для перевозки людей.
/// </summary>
public class PassengerAirplane : Airplane
{
    /// <summary>Базовая ставка стоимости лётного часа, у.е.</summary>
    private const decimal BaseHourRate = 4500m;

    /// <summary>Надбавка за каждое пассажирское место, у.е.</summary>
    private const decimal RatePerSeat = 12m;

    /// <summary>Количество пассажирских мест.</summary>
    public int SeatCount { get; }

    /// <summary>Наличие салона бизнес-класса.</summary>
    public bool HasBusinessClass { get; }

    /// <summary>
    /// Создаёт пассажирский самолёт.
    /// </summary>
    /// <param name="registrationNumber">Бортовой номер.</param>
    /// <param name="model">Модель.</param>
    /// <param name="manufacturer">Производитель.</param>
    /// <param name="engine">Двигатель.</param>
    /// <param name="maxTakeoffWeightKg">Максимальная взлётная масса, кг.</param>
    /// <param name="maxSpeedKmH">Максимальная скорость, км/ч.</param>
    /// <param name="maxRangeKm">Максимальная дальность, км.</param>
    /// <param name="wingspanM">Размах крыла, м.</param>
    /// <param name="engineCount">Количество двигателей.</param>
    /// <param name="requiredRunwayLengthM">Требуемая длина ВПП, м.</param>
    /// <param name="seatCount">Количество пассажирских мест.</param>
    /// <param name="hasBusinessClass">Наличие бизнес-класса.</param>
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

    /// <summary>
    /// Рассчитывает стоимость лётного часа исходя из вместимости салона.
    /// Наличие бизнес-класса увеличивает стоимость обслуживания на 15%.
    /// </summary>
    /// <returns>Стоимость лётного часа, у.е.</returns>
    public override decimal CalculateFlightHourCost()
    {
        decimal cost = BaseHourRate + SeatCount * RatePerSeat;

        if (HasBusinessClass)
        {
            cost *= 1.15m;
        }

        return decimal.Round(cost, 2);
    }

    /// <summary>
    /// Возвращает описание назначения пассажирского самолёта.
    /// </summary>
    public override string DescribePurpose() =>
        $"Пассажирский самолёт на {SeatCount} мест" +
        (HasBusinessClass ? " с салоном бизнес-класса." : ".");

    /// <summary>
    /// Рассчитывает выручку рейса при заданной загрузке и цене билета.
    /// </summary>
    /// <param name="loadFactor">Коэффициент загрузки от 0 до 1.</param>
    /// <param name="ticketPrice">Цена одного билета, у.е.</param>
    /// <returns>Ожидаемая выручка рейса.</returns>
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