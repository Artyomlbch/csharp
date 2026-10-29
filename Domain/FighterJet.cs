using AircraftIndustry.Components;

namespace AircraftIndustry.Domain;

// Боевой самолёт (истребитель).
public class FighterJet : Airplane
{
    // Базовая ставка стоимости лётного часа, у.е.
    private const decimal BaseHourRate = 18000m;

    // Надбавка за каждую единицу точки подвески вооружения, у.е.
    private const decimal RatePerHardpoint = 400m;

    // Количество точек подвески вооружения.
    public int HardpointCount { get; }

    // Максимальное число Маха.
    public double MaxMachNumber { get; }

    // Боевой радиус действия, км.
    public double CombatRadiusKm { get; }

    // Создаёт боевой самолёт.
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

    // Рассчитывает стоимость лётного часа боевого самолёта.
    // Сверхзвуковые машины требуют повышенных затрат на обслуживание.
    public override decimal CalculateFlightHourCost()
    {
        decimal cost = BaseHourRate + HardpointCount * RatePerHardpoint;

        if (MaxMachNumber > 1.5)
        {
            cost *= 1.30m;
        }

        return decimal.Round(cost, 2);
    }

    // Возвращает описание назначения боевого самолёта.
    public override string DescribePurpose() =>
        $"Боевой самолёт с {HardpointCount} точками подвески, " +
        $"максимальная скорость {MaxMachNumber:F1} Маха.";

    // Проверяет возможность выполнения боевой задачи на заданной дальности
    // с учётом возврата на аэродром.
    public bool CanReachTarget(double targetDistanceKm) => targetDistanceKm <= CombatRadiusKm;
}