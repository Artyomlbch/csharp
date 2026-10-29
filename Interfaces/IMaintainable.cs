using AircraftIndustry.Components;

namespace AircraftIndustry.Interfaces;

// Описывает объект, подлежащий техническому обслуживанию.
public interface IMaintainable
{
    // История проведённых обслуживаний.
    IReadOnlyList<MaintenanceRecord> MaintenanceHistory { get; }

    bool NeedsMaintenance { get; }

    // Регистрирует проведённое техническое обслуживание.
    void PerformMaintenance(MaintenanceRecord record);
}