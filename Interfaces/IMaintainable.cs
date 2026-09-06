using AircraftIndustry.Components;

namespace AircraftIndustry.Interfaces;

/// <summary>
/// Описывает объект, подлежащий техническому обслуживанию.
/// </summary>
public interface IMaintainable
{
    /// <summary>История проведённых обслуживаний.</summary>
    IReadOnlyList<MaintenanceRecord> MaintenanceHistory { get; }

    bool NeedsMaintenance { get; }

    /// <summary>
    /// Регистрирует проведённое техническое обслуживание.
    /// </summary>
    void PerformMaintenance(MaintenanceRecord record);
}