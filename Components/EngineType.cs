namespace AircraftIndustry.Components;

/// <summary> тип авиационного двигателя </summary>
public enum EngineType
{
    /// <summary> турбовентиляторный (пассажирские и грузовые самолеты) </summary>
    Turbofan,
    /// <summary> турбовинотовой </summary>
    Turboprop,
    /// <summary> турбовальный (вертолеты) </summary>
    Turboshaft,
    /// <summary> поршневой </summary>
    Piston
}