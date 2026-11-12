namespace AircraftIndustry.Logging;

// вывод лога в файл, строки дописываются в конец
public class FileLogWriter : ILogWriter
{
    // путь к файлу лога
    private readonly string _path;

    public FileLogWriter(string path)
    {
        _path = path;
    }

    public void Write(string message)
    {
        File.AppendAllText(_path, message + Environment.NewLine);
    }
}
