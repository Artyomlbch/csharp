namespace AircraftIndustry.Storage;

// Заявка на выполнение рейса. Описывает требования,
// которым должен удовлетворять подбираемый летательный аппарат.
public class FlightRequest
{
    // Пункт вылета.
    public string DeparturePoint { get; }

    // Пункт назначения.
    public string Destination { get; }

    // Расстояние маршрута, км.
    public double DistanceKm { get; }

    // Требуемое количество пассажирских мест. Ноль, если рейс грузовой.
    public int RequiredSeats { get; }

    // Требуемая грузоподъёмность, кг. Ноль, если рейс пассажирский.
    public double RequiredPayloadKg { get; }

    // Длина доступной взлётно-посадочной полосы, м.
    public double AvailableRunwayLengthM { get; }

    // Создаёт заявку на рейс.
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

    // Оценивает продолжительность рейса при заданной крейсерской скорости.
    public double EstimateDurationHours(double cruiseSpeedKmH)
    {
        if (cruiseSpeedKmH <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(cruiseSpeedKmH), "Скорость должна быть положительной.");
        }

        return DistanceKm / cruiseSpeedKmH;
    }

    // Возвращает строковое представление заявки.
    public override string ToString() =>
        $"{DeparturePoint} → {Destination}, {DistanceKm:N0} км, " +
        $"мест: {RequiredSeats}, груз: {RequiredPayloadKg:N0} кг";
}