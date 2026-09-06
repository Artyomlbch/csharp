using AircraftIndustry.Components;

namespace AircraftIndustry.Domain;

public abstract class Helicopter : Aircraft
{
    public double MainRotorDiameterM { get; }

    public int BladeCount { get; }

    public double HoverCeilingM { get; }

    public override string Category => "Вертолёт";

    protected Helicopter(
        string registrationNumber,
        string model,
        Manufacturer manufacturer,
        Engine engine,
        double maxTakeoffWeightKg,
        double maxSpeedKmH,
        double maxRangeKm,
        double mainRotorDiameterM,
        int bladeCount,
        double hoverCeilingM)
        : base(registrationNumber, model, manufacturer, engine,
               maxTakeoffWeightKg, maxSpeedKmH, maxRangeKm)
    {
        MainRotorDiameterM = mainRotorDiameterM;
        BladeCount = bladeCount;
        HoverCeilingM = hoverCeilingM;
    }

    public double GetRotorDiscAreaM2() => Math.PI * Math.Pow(MainRotorDiameterM / 2, 2);

    public bool CanHoverAt(double altitudeM) => altitudeM <= HoverCeilingM;

    public void Hover(double altitudeM)
    {
        if (!IsInFlight)
        {
            Console.WriteLine($"[{RegistrationNumber}] Зависание невозможно: аппарат на земле.");
            return;
        }

        if (!CanHoverAt(altitudeM))
        {
            Console.WriteLine(
                $"[{RegistrationNumber}] Высота {altitudeM:N0} м превышает потолок зависания " +
                $"({HoverCeilingM:N0} м).");
            return;
        }

        Console.WriteLine($"[{RegistrationNumber}] Зависание на высоте {altitudeM:N0} м.");
    }
}