using AircraftIndustry.Domain;
using AircraftIndustry.Interfaces;

namespace AircraftIndustry.Collections;

// отбирает аппараты с дальностью полёта не меньше заданной
// написан для любого Aircraft
public class MinRangeFilter : IAircraftFilter<Aircraft>
{
    // минимальная дальность полёта, км
    private readonly double _minRangeKm;

    public MinRangeFilter(double minRangeKm)
    {
        _minRangeKm = minRangeKm;
    }

    public bool IsMatch(Aircraft aircraft) => aircraft.MaxRangeKm >= _minRangeKm;
}
