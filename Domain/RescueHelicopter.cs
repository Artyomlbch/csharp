using AircraftIndustry.Components;

namespace AircraftIndustry.Domain;

// поисково-спасательный вертолёт
public class RescueHelicopter : Helicopter
{
    // базовая ставка стоимости лётного часа, у.е
    private const decimal BaseHourRate = 4100m;
    // надбавка за каждое место для пострадавших, у.е
    private const decimal RatePerStretcher = 150m;

    // количество мест для носилок
    public int StretcherCount { get; }

    // наличие спасательной лебёдки
    public bool HasRescueHoist { get; }

    // наличие оборудования для ночных полётов
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


    // рассчитывает стоимость лётного часа с учётом спасательного оборудования
    // стоимость лётного часа, у.е
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

    // возвращает описание назначения спасательного вертолёта
    public override string DescribePurpose() =>
        $"Поисково-спасательный вертолёт на {StretcherCount} носилок" +
        (HasRescueHoist ? " с лебёдкой." : ".");

    // проверяет готовность к выполнению спасательной операции в заданных условиях
    public bool IsReadyForMission(bool isNightTime, bool requiresHoisting)
    {
        if (isNightTime && !HasNightVisionEquipment)
        {
            return false;
        }

        return !requiresHoisting || HasRescueHoist;
    }
}