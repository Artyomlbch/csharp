using AircraftIndustry.Components;
using AircraftIndustry.Domain;
using AircraftIndustry.Storage;

namespace AircraftIndustry.UI;

// Консольный интерфейс взаимодействия с ангаром.
public class ConsoleMenu
{
    // Ангар, с которым работает пользователь.
    private readonly Hangar _hangar;

    // Создаёт меню для работы с заданным ангаром.
    public ConsoleMenu(Hangar hangar)
    {
        _hangar = hangar;
    }

    // Запускает главный цикл работы приложения.
    public void Run()
    {
        bool exitRequested = false;

        while (!exitRequested)
        {
            PrintMenu();
            string? choice = Console.ReadLine();

            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    ShowFleet();
                    break;
                case "2":
                    ShowPolymorphismDemo();
                    break;
                case "3":
                    ShowFlightHourCosts();
                    break;
                case "4":
                    SearchByManufacturer();
                    break;
                case "5":
                    ProcessFlightRequest();
                    break;
                case "6":
                    ShowMaintenanceDemo();
                    break;
                case "7":
                    ShowStatistics();
                    break;
                case "0":
                    exitRequested = true;
                    Console.WriteLine("Работа завершена.");
                    break;
                default:
                    Console.WriteLine("Неизвестный пункт меню.");
                    break;
            }

            Console.WriteLine();
        }
    }

    // Выводит список доступных команд.
    private void PrintMenu()
    {
        Console.WriteLine(_hangar);
        Console.WriteLine("1. Показать парк техники");
        Console.WriteLine("2. Демонстрация полиморфизма");
        Console.WriteLine("3. Стоимость лётного часа");
        Console.WriteLine("4. Поиск по производителю");
        Console.WriteLine("5. Подбор техники под заявку");
        Console.WriteLine("6. Наработка и техобслуживание");
        Console.WriteLine("7. Статистика парка");
        Console.WriteLine("0. Выход");
        Console.Write("Выберите пункт: ");
    }

    // Выводит полный список аппаратов ангара.
    private void ShowFleet()
    {
        Console.WriteLine("Парк техники:");

        foreach (Aircraft aircraft in _hangar.Fleet)
        {
            Console.WriteLine(aircraft);
        }
    }

    // Демонстрирует полиморфное поведение: одинаковый вызов
    // приводит к выполнению реализации конкретного типа техники.
    private void ShowPolymorphismDemo()
    {
        foreach (Aircraft aircraft in _hangar.Fleet)
        {
            Console.WriteLine($"{aircraft.RegistrationNumber}: {aircraft.DescribePurpose()}");
        }

        Console.WriteLine();

        foreach (Aircraft aircraft in _hangar.Fleet)
        {
            aircraft.TakeOff();

            if (aircraft is Helicopter helicopter)
            {
                helicopter.Hover(1500);
            }
            else if (aircraft is Airplane airplane)
            {
                Console.WriteLine(
                    $"[{airplane.RegistrationNumber}] Суммарная тяга: " +
                    $"{airplane.GetTotalThrustKn():F1} кН");
            }

            aircraft.Land();

            Console.WriteLine();
        }
    }

    // Выводит стоимость лётного часа по каждому аппарату и суммарно.
    private void ShowFlightHourCosts()
    {
        Console.WriteLine("Стоимость лётного часа:");

        foreach (Aircraft aircraft in _hangar.Fleet)
        {
            Console.WriteLine(
                $"{aircraft.Model,-28} {aircraft.CalculateFlightHourCost(),12:N2} у.е.");
        }

        Console.WriteLine($"{"Итого по парку",-28} {_hangar.CalculateTotalFleetHourCost(),12:N2} у.е.");
    }

    // Выполняет поиск техники по названию производителя.
    private void SearchByManufacturer()
    {
        Console.Write("Введите название производителя: ");
        string? name = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("Название не задано.");
            return;
        }

        List<Aircraft> found = _hangar.FindByManufacturer(name.Trim());

        if (found.Count == 0)
        {
            Console.WriteLine("Техника указанного производителя не найдена.");
            return;
        }

        foreach (Aircraft aircraft in found)
        {
            Console.WriteLine(aircraft);
        }
    }

    // Демонстрирует подбор техники под заявку на рейс.
    private void ProcessFlightRequest()
    {
        FlightRequest passengerRequest = new(
            departurePoint: "Рига",
            destination: "Барселона",
            distanceKm: 2200,
            requiredSeats: 140,
            requiredPayloadKg: 0,
            availableRunwayLengthM: 3200);

        FlightRequest cargoRequest = new(
            departurePoint: "Рига",
            destination: "Стамбул",
            distanceKm: 2100,
            requiredSeats: 0,
            requiredPayloadKg: 30000,
            availableRunwayLengthM: 3000);

        ShowRequestResult(passengerRequest);
        Console.WriteLine();
        ShowRequestResult(cargoRequest);
    }

    // Выводит результат подбора техники под конкретную заявку.
    private void ShowRequestResult(FlightRequest request)
    {
        Console.WriteLine($"Заявка: {request}");

        List<Aircraft> suitable = _hangar.FindSuitableAircraft(request);

        if (suitable.Count == 0)
        {
            Console.WriteLine("  Подходящей техники не найдено.");
            return;
        }

        foreach (Aircraft aircraft in suitable)
        {
            double hours = request.EstimateDurationHours(aircraft.MaxSpeedKmH);
            decimal flightCost = aircraft.CalculateFlightHourCost() * (decimal)hours;

            Console.WriteLine(
                $"  {aircraft.Model}: {hours:F1} ч, стоимость рейса {flightCost:N2} у.е.");
        }
    }

    // Демонстрирует учёт наработки и техническое обслуживание.
    private void ShowMaintenanceDemo()
    {
        Console.WriteLine("Регистрация налёта:");

        Random random = new(42);

        foreach (Aircraft aircraft in _hangar.Fleet)
        {
            int hours = random.Next(100, 800);
            aircraft.RegisterFlight(hours);

            Console.WriteLine(
                $"{aircraft.RegistrationNumber}: налёт {aircraft.TotalFlightHours} ч, " +
                $"ТО {(aircraft.NeedsMaintenance ? "требуется" : "не требуется")}");
        }

        Console.WriteLine();
        Console.WriteLine("Проведение обслуживания:");

        List<Aircraft> needMaintenance = _hangar.GetAircraftNeedingMaintenance();

        if (needMaintenance.Count == 0)
        {
            Console.WriteLine("Вся техника исправна.");
            return;
        }

        foreach (Aircraft aircraft in needMaintenance)
        {
            MaintenanceRecord record = new(
                date: DateTime.Now,
                description: "Плановое обслуживание по наработке",
                cost: aircraft.CalculateFlightHourCost() * 3,
                flightHoursAtService: aircraft.TotalFlightHours);

            aircraft.PerformMaintenance(record);
            Console.WriteLine($"  {record}");
        }
    }

    // Выводит статистику по парку техники.
    private void ShowStatistics()
    {
        Console.WriteLine("Статистика парка:");

        foreach (KeyValuePair<string, int> entry in _hangar.GetFleetStatistics())
        {
            Console.WriteLine($"{entry.Key}: {entry.Value} шт.");
        }

        Aircraft? longestRange = _hangar.GetLongestRangeAircraft();

        if (longestRange is not null)
        {
            Console.WriteLine(
                $"Наибольшая дальность: {longestRange.Model} " +
                $"({longestRange.MaxRangeKm:N0} км)");
        }

        Console.WriteLine($"Свободных мест в ангаре: {_hangar.FreePlaces}");
    }
}