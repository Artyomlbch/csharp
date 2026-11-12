namespace AircraftIndustry.Components;

public class Engine
{
    private double _thrustKn;
    private int _serviceLifeHours;

    public string Model { get; }
    public EngineType Type { get; }

    public double ThrustKn
    {
        get => _thrustKn;
        set
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(value), "Thrust must be positive."
                );
            }
            _thrustKn = value;
        }
    }

    public int ServiceLifeHours
    {
        get => _serviceLifeHours;
        set
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(value), "Can't be negative."
                );
            }
            _serviceLifeHours = value;
        }
    }

    public Engine(string model, EngineType type, double thrustKn, int serviceLifeHours)
    {
        Model = model;
        Type = type;
        ThrustKn = thrustKn;
        ServiceLifeHours = serviceLifeHours;
    }

    public override string ToString() =>
        $"{Model} [{Type}], thrust {ThrustKn:F1} kN, resource {ServiceLifeHours} h";
}