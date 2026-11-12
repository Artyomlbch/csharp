using System.Runtime.CompilerServices;

namespace AircraftIndustry.Interfaces;

// any flyable object
public interface IFlyable
{
    // max speed in km/h
    double MaxSpeedKmH { get; }
    // max flight range in km
    double MaxRangeKm { get; }
    // is vessel in flight
    bool IsInFlight { get; }

    // to take off
    void TakeOff();
    // to land
    void Land();
}