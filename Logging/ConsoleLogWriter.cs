namespace AircraftIndustry.Logging;

// вывод лога в консоль
public class ConsoleLogWriter : ILogWriter
{
    public void Write(string message)
    {
        Console.WriteLine(message);
    }
}
