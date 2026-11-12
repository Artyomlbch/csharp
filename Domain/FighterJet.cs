using AircraftIndustry.Components;

namespace AircraftIndustry.Domain;

// боевой самолёт (истребитель)
public class FighterJet : Airplane
{
    // базовая ставка стоимости лётного часа, у.е
    private const decimal BaseHourRate = 18000m;

    // надбавка за каждую единицу точки подвески вооружения, у.е
    private const decimal RatePerHardpoint = 400m;

    // количество точек подвески вооружения
    public int HardpointCount { get; }

    // максимальное число Маха
    public double MaxMachNumber { get; }

    // боевой радиус действия, км
    public double CombatRadiusKm { get; }

    // создаёт боевой самолёт
    public FighterJet(
        string registrationNumber,
        string model,
        Manufacturer manufacturer,
        Engine engine,
        double maxTakeoffWeightKg,
        double maxSpeedKmH,
        double maxRangeKm,
        double wingspanM,
        int engineCount,
        double requiredRunwayLengthM,
        int hardpointCount,
        double maxMachNumber,
        double combatRadiusKm)
        : base(registrationNumber, model, manufacturer, engine, maxTakeoffWeightKg,
               maxSpeedKmH, maxRangeKm, wingspanM, engineCount, requiredRunwayLengthM)
    {
        HardpointCount = hardpointCount;
        MaxMachNumber = maxMachNumber;
        CombatRadiusKm = combatRadiusKm;
    }

    // рассчитывает стоимость лётного часа боевого самолёта
    // сверхзвуковые машины требуют повышенных затрат на обслуживание
    public override decimal CalculateFlightHourCost()
    {
        decimal cost = BaseHourRate + HardpointCount * RatePerHardpoint;

        if (MaxMachNumber > 1.5)
        {
            cost *= 1.30m;
        }

        return decimal.Round(cost, 2);
    }

    // возвращает описание назначения боевого самолёта
    public override string DescribePurpose() =>
        $"Боевой самолёт с {HardpointCount} точками подвески, " +
        $"максимальная скорость {MaxMachNumber:F1} Маха.";

    // проверяет возможность выполнения боевой задачи на заданной дальности
    // с учётом возврата на аэродром
    public bool CanReachTarget(double targetDistanceKm) => targetDistanceKm <= CombatRadiusKm;
}