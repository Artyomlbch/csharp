using System.Globalization;
using AircraftIndustry.Storage;
using AircraftIndustry.UI;

namespace AircraftIndustry;

public class Program
{
    public static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        CultureInfo.CurrentCulture = new CultureInfo("ru-RU");

        Hangar hangar = FleetFactory.CreateDemoHangar();
        ConsoleMenu menu = new(hangar);

        menu.Run();
    }
}