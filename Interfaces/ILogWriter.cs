namespace AircraftIndustry.Logging;

// единый интерфейс источников вывода лога
public interface ILogWriter
{
    // печатает строку лога
    void Write(string message);
}
