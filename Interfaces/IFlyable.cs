using System.Runtime.CompilerServices;

namespace AircraftIndustry.Interfaces;

// Any flyable object
public interface IFlyable
{
    // Max speed in km/h
    double MaxSpeedKmH { get; }
    // Max flight range in km
    double MaxRangeKm { get; }
    // Is vessel in flight
    bool IsInFlight { get; }

    // To take off
    void TakeOff();
    // To land
    void Land();
}