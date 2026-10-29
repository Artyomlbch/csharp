using AircraftIndustry.Components;

namespace AircraftIndustry.Domain;

public abstract class Airplane : Aircraft
{
    // размах крыла
    public double WingspanM { get; }

    // кол-во двигателей
    public int EngineCount { get; }

    // минимальная длина взлетно-посадочной полосы
    public double RequiredRunwayLengthM { get; }

    public override string Category => "Самолёт";

    protected Airplane(
        string registrationNumber,
        string model,
        Manufacturer manufacturer,
        Engine engine,
        double maxTakeoffWeightKg,
        double maxSpeedKmH,
        double maxRangeKm,
        double wingspanM,
        int engineCount,
        double requiredRunwayLengthM)
        : base(registrationNumber, model, manufacturer, engine,
               maxTakeoffWeightKg, maxSpeedKmH, maxRangeKm)
    {
        WingspanM = wingspanM;
        EngineCount = engineCount;
        RequiredRunwayLengthM = requiredRunwayLengthM;
    }

    public double GetTotalThrustKn() => Engine.ThrustKn * EngineCount;
    public bool CanUseRunway(double runwayLengthM) => runwayLengthM >= RequiredRunwayLengthM;

    public override string ToString()
    {
        return base.ToString() + $", размах {WingspanM:F1} м, ВПП от {RequiredRunwayLengthM:N0} м";
    }
}