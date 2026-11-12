using System.Globalization;
using AircraftIndustry.Logging;
using AircraftIndustry.Storage;
using AircraftIndustry.UI;

namespace AircraftIndustry;

public class Program
{
    public static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        CultureInfo.CurrentCulture = new CultureInfo("ru-RU");

        Logger logger = new();
        ILogWriter consoleWriter = new ConsoleLogWriter();
        ILogWriter fileWriter = new FileLogWriter("log.txt");

        // подписываем источники вывода на событие логгера
        logger.MessageLogged += consoleWriter.Write;
        logger.MessageLogged += fileWriter.Write;

        logger.Log("Программа запущена");

        Hangar hangar = FleetFactory.CreateDemoHangar();
        logger.Log("Загружено аппаратов в ангар", hangar.OccupiedPlaces);

        ConsoleMenu menu = new(hangar, logger);
        menu.Run();

        logger.Log("Программа завершена");
    }
}