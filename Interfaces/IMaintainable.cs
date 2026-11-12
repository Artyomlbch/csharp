using AircraftIndustry.Components;

namespace AircraftIndustry.Interfaces;

// описывает объект, подлежащий техническому обслуживанию
public interface IMaintainable
{
    // история проведённых обслуживаний
    IReadOnlyList<MaintenanceRecord> MaintenanceHistory { get; }

    bool NeedsMaintenance { get; }

    // регистрирует проведённое техническое обслуживание
    void PerformMaintenance(MaintenanceRecord record);
}