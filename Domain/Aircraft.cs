using AircraftIndustry.Components;
using AircraftIndustry.Interfaces;

namespace AircraftIndustry.Domain;

// базовая абстракция летательного аппарата
// определяет общее состояние и поведение самолётов и вертолётов
public abstract class Aircraft : IFlyable, IMaintainable
{
    private const int MaintenanceIntervalHours = 500;

    private readonly List<MaintenanceRecord> _maintenanceHistory = new();

    private int _totalFlightHours;

    public string RegistrationNumber { get; }

    public string Model { get; }

    public Manufacturer Manufacturer { get; }

    public Engine Engine { get; }

    public double MaxTakeoffWeightKg { get; }

    public double MaxSpeedKmH { get; }

    public double MaxRangeKm { get; }

    public bool IsInFlight { get; private set; }

    public int TotalFlightHours => _totalFlightHours;

    public IReadOnlyList<MaintenanceRecord> MaintenanceHistory => _maintenanceHistory;

    public bool NeedsMaintenance
    {
        get
        {
            int hoursAtLastService = _maintenanceHistory.Count == 0
                ? 0
                : _maintenanceHistory[^1].FlightHoursAtService;

            return _totalFlightHours - hoursAtLastService >= MaintenanceIntervalHours;
        }
    }

    public abstract string Category { get; }

    protected Aircraft(
        string registrationNumber,
        string model,
        Manufacturer manufacturer,
        Engine engine,
        double maxTakeoffWeightKg,
        double maxSpeedKmH,
        double maxRangeKm)
    {
        RegistrationNumber = registrationNumber;
        Model = model;
        Manufacturer = manufacturer;
        Engine = engine;
        MaxTakeoffWeightKg = maxTakeoffWeightKg;
        MaxSpeedKmH = maxSpeedKmH;
        MaxRangeKm = maxRangeKm;
    }

    public abstract decimal CalculateFlightHourCost();

    public virtual string DescribePurpose() =>
        $"Летательный аппарат общего назначения категории «{Category}».";

    public void TakeOff()
    {
        if (IsInFlight)
        {
            Console.WriteLine($"[{RegistrationNumber}] Аппарат уже находится в воздухе.");
            return;
        }

        IsInFlight = true;
        Console.WriteLine($"[{RegistrationNumber}] {Model}: взлёт выполнен.");
    }

    public void Land()
    {
        if (!IsInFlight)
        {
            Console.WriteLine($"[{RegistrationNumber}] Аппарат находится на земле.");
            return;
        }

        IsInFlight = false;
        Console.WriteLine($"[{RegistrationNumber}] {Model}: посадка выполнена.");
    }

    public void RegisterFlight(int hours)
    {
        if (hours <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(hours), "Продолжительность полёта должна быть положительной.");
        }

        _totalFlightHours += hours;
    }

    public bool CanCoverDistance(double distanceKm) => distanceKm <= MaxRangeKm;

    // регистрирует проведённое техническое обслуживание
    public void PerformMaintenance(MaintenanceRecord record)
    {
        _maintenanceHistory.Add(record);
        Console.WriteLine($"[{RegistrationNumber}] Проведено ТО: {record.Description}");
    }


    public override string ToString() =>
        $"{RegistrationNumber} | {Model} ({Category}), {Manufacturer.Name}, " +
        $"{MaxSpeedKmH:N0} км/ч, дальность {MaxRangeKm:N0} км";
}