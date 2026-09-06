using AircraftIndustry.Components;

namespace AircraftIndustry.Domain;

/// <summary>
/// Грузовой самолёт, предназначенный для перевозки грузов.
/// </summary>
public class CargoAirplane : Airplane
{
    /// <summary>Базовая ставка стоимости лётного часа, у.е.</summary>
    private const decimal BaseHourRate = 5200m;

    /// <summary>Надбавка за каждую тонну грузоподъёмности, у.е.</summary>
    private const decimal RatePerTon = 95m;

    /// <summary>Максимальная масса перевозимого груза, кг.</summary>
    public double MaxPayloadKg { get; }

    /// <summary>Объём грузового отсека, куб. м.</summary>
    public double CargoBayVolumeM3 { get; }

    /// <summary>Наличие грузовой рампы для самопогрузки.</summary>
    public bool HasLoadingRamp { get; }

    /// <summary>
    /// Создаёт грузовой самолёт.
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
    /// <param name="maxPayloadKg">Максимальная масса груза, кг.</param>
    /// <param name="cargoBayVolumeM3">Объём грузового отсека, куб. м.</param>
    /// <param name="hasLoadingRamp">Наличие грузовой рампы.</param>
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

    /// <summary>
    /// Рассчитывает стоимость лётного часа исходя из грузоподъёмности.
    /// </summary>
    /// <returns>Стоимость лётного часа, у.е.</returns>
    public override decimal CalculateFlightHourCost()
    {
        decimal payloadTons = (decimal)(MaxPayloadKg / 1000);
        decimal cost = BaseHourRate + payloadTons * RatePerTon;

        return decimal.Round(cost, 2);
    }

    /// <summary>
    /// Возвращает описание назначения грузового самолёта.
    /// </summary>
    public override string DescribePurpose() =>
        $"Грузовой самолёт грузоподъёмностью {MaxPayloadKg / 1000:F1} т " +
        $"и объёмом отсека {CargoBayVolumeM3:N0} куб. м.";

    /// <summary>
    /// Проверяет возможность размещения груза заданной массы и объёма.
    /// </summary>
    /// <param name="weightKg">Масса груза, кг.</param>
    /// <param name="volumeM3">Объём груза, куб. м.</param>
    /// <returns>true, если груз помещается по массе и по объёму.</returns>
    public bool CanCarry(double weightKg, double volumeM3) =>
        weightKg <= MaxPayloadKg && volumeM3 <= CargoBayVolumeM3;
}