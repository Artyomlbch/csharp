using AircraftIndustry.Components;

namespace AircraftIndustry.Domain;

/// <summary>
/// Поисково-спасательный вертолёт.
/// </summary>
public class RescueHelicopter : Helicopter
{
    // Базовая ставка стоимости лётного часа, у.е.
    private const decimal BaseHourRate = 4100m;
    // Надбавка за каждое место для пострадавших, у.e
    private const decimal RatePerStretcher = 150m;

    // Количество мест для носилок
    public int StretcherCount { get; }

    // Наличие спасательной лебёдки
    public bool HasRescueHoist { get; }

    // Наличие оборудования для ночных полётов.
    public bool HasNightVisionEquipment { get; }

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


    // Рассчитывает стоимость лётного часа с учётом спасательного оборудования.
    // Стоимость лётного часа, у.е.
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

    // Возвращает описание назначения спасательного вертолёта.
    public override string DescribePurpose() =>
        $"Поисково-спасательный вертолёт на {StretcherCount} носилок" +
        (HasRescueHoist ? " с лебёдкой." : ".");

    /// <summary>
    /// Проверяет готовность к выполнению спасательной операции в заданных условиях.
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