using System.Runtime.CompilerServices;

namespace AircraftIndustry.Interfaces;

/// <summary>
/// Any flyable object
/// </summary>
public interface IFlyable
{
    /// <summary> Max speed in km/h </summary>
    double MaxSpeedKmH { get; }
    /// <summary> Max flight range in km </summary>
    double MaxRangeKm { get; }
    /// <summary> Is vessel in flight </summary>
    bool IsInFlight { get; }

    /// <summary> To take off </summary>
    void TakeOff();
    /// <summary> To land </summary>
    void Land();
}