using AircraftIndustry.Components;
using AircraftIndustry.Domain;

namespace AircraftIndustry.Storage;

/// <summary>
/// Фабрика создания демонстрационного парка летательных аппаратов.
/// Отделяет наполнение данными от бизнес-логики ангара.
/// </summary>
public static class FleetFactory
{
    /// <summary>
    /// Создаёт ангар, заполненный демонстрационным парком техники.
    /// </summary>
    /// <returns>Ангар с зарегистрированными аппаратами.</returns>
    public static Hangar CreateDemoHangar()
    {
        Hangar hangar = new("Северный", "Аэродром Румбула", 12);

        foreach (Aircraft aircraft in CreateDemoFleet())
        {
            hangar.Register(aircraft);
        }

        return hangar;
    }

    /// <summary>
    /// Формирует список демонстрационных летательных аппаратов.
    /// </summary>
    /// <returns>Список из семи аппаратов разных типов.</returns>
    public static List<Aircraft> CreateDemoFleet()
    {
        Manufacturer boeing = new("Boeing", "США", 1916);
        Manufacturer airbus = new("Airbus", "Франция", 1970);
        Manufacturer lockheed = new("Lockheed Martin", "США", 1912);
        Manufacturer leonardo = new("Leonardo", "Италия", 1948);
        Manufacturer sikorsky = new("Sikorsky", "США", 1923);

        return new List<Aircraft>
        {
            new PassengerAirplane(
                registrationNumber: "YL-BAA",
                model: "Airbus A220-300",
                manufacturer: airbus,
                engine: new Engine("PW1500G", EngineType.Turbofan, 103.0, 20000),
                maxTakeoffWeightKg: 69900,
                maxSpeedKmH: 871,
                maxRangeKm: 6297,
                wingspanM: 35.1,
                engineCount: 2,
                requiredRunwayLengthM: 1890,
                seatCount: 145,
                hasBusinessClass: true),

            new PassengerAirplane(
                registrationNumber: "YL-BAB",
                model: "Boeing 737-800",
                manufacturer: boeing,
                engine: new Engine("CFM56-7B", EngineType.Turbofan, 121.4, 25000),
                maxTakeoffWeightKg: 79010,
                maxSpeedKmH: 842,
                maxRangeKm: 5765,
                wingspanM: 35.8,
                engineCount: 2,
                requiredRunwayLengthM: 2100,
                seatCount: 189,
                hasBusinessClass: false),

            new CargoAirplane(
                registrationNumber: "YL-CGA",
                model: "Boeing 767-300F",
                manufacturer: boeing,
                engine: new Engine("CF6-80C2", EngineType.Turbofan, 273.0, 30000),
                maxTakeoffWeightKg: 186880,
                maxSpeedKmH: 850,
                maxRangeKm: 6025,
                wingspanM: 47.6,
                engineCount: 2,
                requiredRunwayLengthM: 2800,
                maxPayloadKg: 52700,
                cargoBayVolumeM3: 438,
                hasLoadingRamp: false),

            new CargoAirplane(
                registrationNumber: "YL-CGB",
                model: "Airbus A400M",
                manufacturer: airbus,
                engine: new Engine("TP400-D6", EngineType.Turboprop, 80.0, 15000),
                maxTakeoffWeightKg: 141000,
                maxSpeedKmH: 780,
                maxRangeKm: 3300,
                wingspanM: 42.4,
                engineCount: 4,
                requiredRunwayLengthM: 980,
                maxPayloadKg: 37000,
                cargoBayVolumeM3: 356,
                hasLoadingRamp: true),

            new FighterJet(
                registrationNumber: "YL-FJ1",
                model: "F-16C Fighting Falcon",
                manufacturer: lockheed,
                engine: new Engine("F110-GE-129", EngineType.Turbofan, 131.0, 8000),
                maxTakeoffWeightKg: 19187,
                maxSpeedKmH: 2120,
                maxRangeKm: 4220,
                wingspanM: 9.96,
                engineCount: 1,
                requiredRunwayLengthM: 1500,
                hardpointCount: 11,
                maxMachNumber: 2.0,
                combatRadiusKm: 550),

            new TransportHelicopter(
                registrationNumber: "YL-HTA",
                model: "CH-53K King Stallion",
                manufacturer: sikorsky,
                engine: new Engine("T408-GE-400", EngineType.Turboshaft, 57.0, 6000),
                maxTakeoffWeightKg: 41000,
                maxSpeedKmH: 315,
                maxRangeKm: 1000,
                mainRotorDiameterM: 24.0,
                bladeCount: 7,
                hoverCeilingM: 4200,
                maxPayloadKg: 13600,
                externalSlingCapacityKg: 16330),

            new RescueHelicopter(
                registrationNumber: "YL-HRA",
                model: "AW139",
                manufacturer: leonardo,
                engine: new Engine("PT6C-67C", EngineType.Turboshaft, 12.0, 5000),
                maxTakeoffWeightKg: 6800,
                maxSpeedKmH: 306,
                maxRangeKm: 1061,
                mainRotorDiameterM: 13.8,
                bladeCount: 5,
                hoverCeilingM: 2100,
                stretcherCount: 4,
                hasRescueHoist: true,
                hasNightVisionEquipment: true)
        };
    }
}