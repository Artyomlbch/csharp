using AircraftIndustry.Components;

namespace AircraftIndustry.Domain;

// грузовой самолёт, предназначенный для перевозки грузов
public class CargoAirplane : Airplane
{
    // базовая ставка стоимости лётного часа, у.е
    private const decimal BaseHourRate = 5200m;

    // надбавка за каждую тонну грузоподъёмности, у.е
    private const decimal RatePerTon = 95m;

    // максимальная масса перевозимого груза, кг
    public double MaxPayloadKg { get; }

    // объём грузового отсека, куб. м
    public double CargoBayVolumeM3 { get; }

    // наличие грузовой рампы для самопогрузки
    public bool HasLoadingRamp { get; }

    // создаёт грузовой самолёт
    public CargoAirplane(
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
        double maxPayloadKg,
        double cargoBayVolumeM3,
        bool hasLoadingRamp)
        : base(registrationNumber, model, manufacturer, engine, maxTakeoffWeightKg,
               maxSpeedKmH, maxRangeKm, wingspanM, engineCount, requiredRunwayLengthM)
    {
        MaxPayloadKg = maxPayloadKg;
        CargoBayVolumeM3 = cargoBayVolumeM3;
        HasLoadingRamp = hasLoadingRamp;
    }

    // рассчитывает стоимость лётного часа исходя из грузоподъёмности
    public override decimal CalculateFlightHourCost()
    {
        decimal payloadTons = (decimal)(MaxPayloadKg / 1000);
        decimal cost = BaseHourRate + payloadTons * RatePerTon;

        return decimal.Round(cost, 2);
    }

    // возвращает описание назначения грузового самолёта
    public override string DescribePurpose() =>
        $"Грузовой самолёт грузоподъёмностью {MaxPayloadKg / 1000:F1} т " +
        $"и объёмом отсека {CargoBayVolumeM3:N0} куб. м.";

    // проверяет возможность размещения груза заданной массы и объёма
    public bool CanCarry(double weightKg, double volumeM3) =>
        weightKg <= MaxPayloadKg && volumeM3 <= CargoBayVolumeM3;
}