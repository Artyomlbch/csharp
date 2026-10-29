using AircraftIndustry.Components;

namespace AircraftIndustry.Domain;

// Грузовой самолёт, предназначенный для перевозки грузов.
public class CargoAirplane : Airplane
{
    // Базовая ставка стоимости лётного часа, у.е.
    private const decimal BaseHourRate = 5200m;

    // Надбавка за каждую тонну грузоподъёмности, у.е.
    private const decimal RatePerTon = 95m;

    // Максимальная масса перевозимого груза, кг.
    public double MaxPayloadKg { get; }

    // Объём грузового отсека, куб. м.
    public double CargoBayVolumeM3 { get; }

    // Наличие грузовой рампы для самопогрузки.
    public bool HasLoadingRamp { get; }

    // Создаёт грузовой самолёт.
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

    // Рассчитывает стоимость лётного часа исходя из грузоподъёмности.
    public override decimal CalculateFlightHourCost()
    {
        decimal payloadTons = (decimal)(MaxPayloadKg / 1000);
        decimal cost = BaseHourRate + payloadTons * RatePerTon;

        return decimal.Round(cost, 2);
    }

    // Возвращает описание назначения грузового самолёта.
    public override string DescribePurpose() =>
        $"Грузовой самолёт грузоподъёмностью {MaxPayloadKg / 1000:F1} т " +
        $"и объёмом отсека {CargoBayVolumeM3:N0} куб. м.";

    // Проверяет возможность размещения груза заданной массы и объёма.
    public bool CanCarry(double weightKg, double volumeM3) =>
        weightKg <= MaxPayloadKg && volumeM3 <= CargoBayVolumeM3;
}