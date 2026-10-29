using AircraftIndustry.Components;

namespace AircraftIndustry.Domain;

// транспортный вертолёт для перевозки грузов и личного состава
public class TransportHelicopter : Helicopter
{
    // базовая ставка стоимости лётного часа, у.е
    private const decimal BaseHourRate = 3800m;

    // надбавка за каждую тонну грузоподъёмности, у.е
    private const decimal RatePerTon = 210m;

    // максимальная масса груза в кабине, кг
    public double MaxPayloadKg { get; }

    // максимальная масса груза на внешней подвеске, кг
    public double ExternalSlingCapacityKg { get; }

    // создаёт транспортный вертолёт
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

    // рассчитывает стоимость лётного часа исходя из суммарной грузоподъёмности
    public override decimal CalculateFlightHourCost()
    {
        decimal totalCapacityTons = (decimal)((MaxPayloadKg + ExternalSlingCapacityKg) / 1000);
        decimal cost = BaseHourRate + totalCapacityTons * RatePerTon;

        return decimal.Round(cost, 2);
    }

    // возвращает описание назначения транспортного вертолёта
    public override string DescribePurpose() =>
        $"Транспортный вертолёт: {MaxPayloadKg / 1000:F1} т в кабине, " +
        $"{ExternalSlingCapacityKg / 1000:F1} т на внешней подвеске.";

    // определяет способ перевозки груза заданной массы
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