namespace AircraftIndustry.Storage;

// заявка на выполнение рейса. Описывает требования,
// которым должен удовлетворять подбираемый летательный аппарат
public class FlightRequest
{
    // пункт вылета
    public string DeparturePoint { get; }

    // пункт назначения
    public string Destination { get; }

    // расстояние маршрута, км
    public double DistanceKm { get; }

    // требуемое количество пассажирских мест. Ноль, если рейс грузовой
    public int RequiredSeats { get; }

    // требуемая грузоподъёмность, кг. Ноль, если рейс пассажирский
    public double RequiredPayloadKg { get; }

    // длина доступной взлётно-посадочной полосы, м
    public double AvailableRunwayLengthM { get; }

    // создаёт заявку на рейс
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

    // оценивает продолжительность рейса при заданной крейсерской скорости
    public double EstimateDurationHours(double cruiseSpeedKmH)
    {
        if (cruiseSpeedKmH <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(cruiseSpeedKmH), "Скорость должна быть положительной.");
        }

        return DistanceKm / cruiseSpeedKmH;
    }

    // возвращает строковое представление заявки
    public override string ToString() =>
        $"{DeparturePoint} -> {Destination}, {DistanceKm:N0} км, " +
        $"мест: {RequiredSeats}, груз: {RequiredPayloadKg:N0} кг";
}