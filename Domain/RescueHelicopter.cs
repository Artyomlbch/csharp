using AircraftIndustry.Components;

namespace AircraftIndustry.Domain;

/// <summary>
/// Поисково-спасательный вертолёт.
/// </summary>
public class RescueHelicopter : Helicopter
{
    /// <summary>Базовая ставка стоимости лётного часа, у.е.</summary>
    private const decimal BaseHourRate = 4100m;

    /// <summary>Надбавка за каждое место для пострадавших, у.е.</summary>
    private const decimal RatePerStretcher = 150m;

    /// <summary>Количество мест для носилок.</summary>
    public int StretcherCount { get; }

    /// <summary>Наличие спасательной лебёдки.</summary>
    public bool HasRescueHoist { get; }

    /// <summary>Наличие оборудования для ночных полётов.</summary>
    public bool HasNightVisionEquipment { get; }

    /// <summary>
    /// Создаёт поисково-спасательный вертолёт.
    /// </summary>
    /// <param name="registrationNumber">Бортовой номер.</param>
    /// <param name="model">Модель.</param>
    /// <param name="manufacturer">Производитель.</param>
    /// <param name="engine">Двигатель.</param>
    /// <param name="maxTakeoffWeightKg">Максимальная взлётная масса, кг.</param>
    /// <param name="maxSpeedKmH">Максимальная скорость, км/ч.</param>
    /// <param name="maxRangeKm">Максимальная дальность, км.</param>
    /// <param name="mainRotorDiameterM">Диаметр несущего винта, м.</param>
    /// <param name="bladeCount">Количество лопастей.</param>
    /// <param name="hoverCeilingM">Потолок зависания, м.</param>
    /// <param name="stretcherCount">Количество мест для носилок.</param>
    /// <param name="hasRescueHoist">Наличие спасательной лебёдки.</param>
    /// <param name="hasNightVisionEquipment">Наличие оборудования ночного видения.</param>
    public RescueHelicopter(
        string registrationNumber,
        string model,
        Manufacturer manufacturer,
        Engine engine,
        double maxTakeoffWeightKg,
        double maxSpeedKmH,
        double maxRangeKm,
        double mainRotorDiameterM,
        int bladeCount,
        double hoverCeilingM,
        int stretcherCount,
        bool hasRescueHoist,
        bool hasNightVisionEquipment)
        : base(registrationNumber, model, manufacturer, engine, maxTakeoffWeightKg,
               maxSpeedKmH, maxRangeKm, mainRotorDiameterM, bladeCount, hoverCeilingM)
    {
        StretcherCount = stretcherCount;
        HasRescueHoist = hasRescueHoist;
        HasNightVisionEquipment = hasNightVisionEquipment;
    }

    /// <summary>
    /// Рассчитывает стоимость лётного часа с учётом спасательного оборудования.
    /// </summary>
    /// <returns>Стоимость лётного часа, у.е.</returns>
    public override decimal CalculateFlightHourCost()
    {
        decimal cost = BaseHourRate + StretcherCount * RatePerStretcher;

        if (HasRescueHoist)
        {
            cost += 600m;
        }

        if (HasNightVisionEquipment)
        {
            cost += 850m;
        }

        return decimal.Round(cost, 2);
    }

    /// <summary>
    /// Возвращает описание назначения спасательного вертолёта.
    /// </summary>
    public override string DescribePurpose() =>
        $"Поисково-спасательный вертолёт на {StretcherCount} носилок" +
        (HasRescueHoist ? " с лебёдкой." : ".");

    /// <summary>
    /// Проверяет готовность к выполнению спасательной операции в заданных условиях.
    /// </summary>
    /// <param name="isNightTime">Признак ночного времени суток.</param>
    /// <param name="requiresHoisting">Требуется ли подъём лебёдкой.</param>
    /// <returns>true, если вертолёт оснащён для таких условий.</returns>
    public bool IsReadyForMission(bool isNightTime, bool requiresHoisting)
    {
        if (isNightTime && !HasNightVisionEquipment)
        {
            return false;
        }

        return !requiresHoisting || HasRescueHoist;
    }
}