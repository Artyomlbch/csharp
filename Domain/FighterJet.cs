using AircraftIndustry.Components;

namespace AircraftIndustry.Domain;

/// <summary>
/// Боевой самолёт (истребитель).
/// </summary>
public class FighterJet : Airplane
{
    /// <summary>Базовая ставка стоимости лётного часа, у.е.</summary>
    private const decimal BaseHourRate = 18000m;

    /// <summary>Надбавка за каждую единицу точки подвески вооружения, у.е.</summary>
    private const decimal RatePerHardpoint = 400m;

    /// <summary>Количество точек подвески вооружения.</summary>
    public int HardpointCount { get; }

    /// <summary>Максимальное число Маха.</summary>
    public double MaxMachNumber { get; }

    /// <summary>Боевой радиус действия, км.</summary>
    public double CombatRadiusKm { get; }

    /// <summary>
    /// Создаёт боевой самолёт.
    /// </summary>
    /// <param name="registrationNumber">Бортовой номер.</param>
    /// <param name="model">Модель.</param>
    /// <param name="manufacturer">Производитель.</param>
    /// <param name="engine">Двигатель.</param>
    /// <param name="maxTakeoffWeightKg">Максимальная взлётная масса, кг.</param>
    /// <param name="maxSpeedKmH">Максимальная скорость, км/ч.</param>
    /// <param name="maxRangeKm">Максимальная дальность, км.</param>
    /// <param name="wingspanM">Размах крыла, м.</param>
    /// <param name="engineCount">Количество двигателей.</param>
    /// <param name="requiredRunwayLengthM">Требуемая длина ВПП, м.</param>
    /// <param name="hardpointCount">Количество точек подвески.</param>
    /// <param name="maxMachNumber">Максимальное число Маха.</param>
    /// <param name="combatRadiusKm">Боевой радиус, км.</param>
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

    /// <summary>
    /// Рассчитывает стоимость лётного часа боевого самолёта.
    /// Сверхзвуковые машины требуют повышенных затрат на обслуживание.
    /// </summary>
    /// <returns>Стоимость лётного часа, у.е.</returns>
    public override decimal CalculateFlightHourCost()
    {
        decimal cost = BaseHourRate + HardpointCount * RatePerHardpoint;

        if (MaxMachNumber > 1.5)
        {
            cost *= 1.30m;
        }

        return decimal.Round(cost, 2);
    }

    /// <summary>
    /// Возвращает описание назначения боевого самолёта.
    /// </summary>
    public override string DescribePurpose() =>
        $"Боевой самолёт с {HardpointCount} точками подвески, " +
        $"максимальная скорость {MaxMachNumber:F1} Маха.";

    /// <summary>
    /// Проверяет возможность выполнения боевой задачи на заданной дальности
    /// с учётом возврата на аэродром.
    /// </summary>
    /// <param name="targetDistanceKm">Расстояние до цели, км.</param>
    /// <returns>true, если цель находится в пределах боевого радиуса.</returns>
    public bool CanReachTarget(double targetDistanceKm) => targetDistanceKm <= CombatRadiusKm;
}