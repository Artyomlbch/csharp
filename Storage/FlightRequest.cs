namespace AircraftIndustry.Storage;

/// <summary>
/// Заявка на выполнение рейса. Описывает требования,
/// которым должен удовлетворять подбираемый летательный аппарат.
/// </summary>
public class FlightRequest
{
    /// <summary>Пункт вылета.</summary>
    public string DeparturePoint { get; }

    /// <summary>Пункт назначения.</summary>
    public string Destination { get; }

    /// <summary>Расстояние маршрута, км.</summary>
    public double DistanceKm { get; }

    /// <summary>Требуемое количество пассажирских мест. Ноль, если рейс грузовой.</summary>
    public int RequiredSeats { get; }

    /// <summary>Требуемая грузоподъёмность, кг. Ноль, если рейс пассажирский.</summary>
    public double RequiredPayloadKg { get; }

    /// <summary>Длина доступной взлётно-посадочной полосы, м.</summary>
    public double AvailableRunwayLengthM { get; }

    /// <summary>
    /// Создаёт заявку на рейс.
    /// </summary>
    public FlightRequest(
        string departurePoint,
        string destination,
        double distanceKm,
        int requiredSeats,
        double requiredPayloadKg,
        double availableRunwayLengthM)
    {
        if (distanceKm <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(distanceKm), "Расстояние маршрута должно быть положительным.");
        }

        DeparturePoint = departurePoint;
        Destination = destination;
        DistanceKm = distanceKm;
        RequiredSeats = requiredSeats;
        RequiredPayloadKg = requiredPayloadKg;
        AvailableRunwayLengthM = availableRunwayLengthM;
    }

    /// <summary>
    /// Оценивает продолжительность рейса при заданной крейсерской скорости.
    /// </summary>
    /// <param name="cruiseSpeedKmH">Крейсерская скорость, км/ч.</param>
    /// <returns>Расчётное время в пути, часов.</returns>
    public double EstimateDurationHours(double cruiseSpeedKmH)
    {
        if (cruiseSpeedKmH <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(cruiseSpeedKmH), "Скорость должна быть положительной.");
        }

        return DistanceKm / cruiseSpeedKmH;
    }

    /// <summary>
    /// Возвращает строковое представление заявки.
    /// </summary>
    public override string ToString() =>
        $"{DeparturePoint} → {Destination}, {DistanceKm:N0} км, " +
        $"мест: {RequiredSeats}, груз: {RequiredPayloadKg:N0} кг";
}