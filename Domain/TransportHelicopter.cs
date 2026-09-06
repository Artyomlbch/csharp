using AircraftIndustry.Components;

namespace AircraftIndustry.Domain;

/// <summary>
/// Транспортный вертолёт для перевозки грузов и личного состава.
/// </summary>
public class TransportHelicopter : Helicopter
{
    /// <summary>Базовая ставка стоимости лётного часа, у.е.</summary>
    private const decimal BaseHourRate = 3800m;

    /// <summary>Надбавка за каждую тонну грузоподъёмности, у.е.</summary>
    private const decimal RatePerTon = 210m;

    /// <summary>Максимальная масса груза в кабине, кг.</summary>
    public double MaxPayloadKg { get; }

    /// <summary>Максимальная масса груза на внешней подвеске, кг.</summary>
    public double ExternalSlingCapacityKg { get; }

    /// <summary>
    /// Создаёт транспортный вертолёт.
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
    /// <param name="maxPayloadKg">Масса груза в кабине, кг.</param>
    /// <param name="externalSlingCapacityKg">Масса груза на внешней подвеске, кг.</param>
    public TransportHelicopter(
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
        double maxPayloadKg,
        double externalSlingCapacityKg)
        : base(registrationNumber, model, manufacturer, engine, maxTakeoffWeightKg,
               maxSpeedKmH, maxRangeKm, mainRotorDiameterM, bladeCount, hoverCeilingM)
    {
        MaxPayloadKg = maxPayloadKg;
        ExternalSlingCapacityKg = externalSlingCapacityKg;
    }

    /// <summary>
    /// Рассчитывает стоимость лётного часа исходя из суммарной грузоподъёмности.
    /// </summary>
    /// <returns>Стоимость лётного часа, у.е.</returns>
    public override decimal CalculateFlightHourCost()
    {
        decimal totalCapacityTons = (decimal)((MaxPayloadKg + ExternalSlingCapacityKg) / 1000);
        decimal cost = BaseHourRate + totalCapacityTons * RatePerTon;

        return decimal.Round(cost, 2);
    }

    /// <summary>
    /// Возвращает описание назначения транспортного вертолёта.
    /// </summary>
    public override string DescribePurpose() =>
        $"Транспортный вертолёт: {MaxPayloadKg / 1000:F1} т в кабине, " +
        $"{ExternalSlingCapacityKg / 1000:F1} т на внешней подвеске.";

    /// <summary>
    /// Определяет способ перевозки груза заданной массы.
    /// </summary>
    /// <param name="weightKg">Масса груза, кг.</param>
    /// <returns>Описание доступного способа перевозки.</returns>
    public string DetermineTransportMethod(double weightKg)
    {
        if (weightKg <= MaxPayloadKg)
        {
            return "перевозка в грузовой кабине";
        }

        if (weightKg <= ExternalSlingCapacityKg)
        {
            return "перевозка на внешней подвеске";
        }

        return "груз превышает возможности вертолёта";
    }
}