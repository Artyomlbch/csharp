namespace AircraftIndustry.Logging;

// класс логирования
// в нём описано только событие и методы, которые его вызывают;
// сама печать находится во внешних классах ConsoleLogWriter и FileLogWriter
public class Logger
{
    // событие, на которое подписываются источники вывода
    public event Action<string>? MessageLogged;

    // обобщённый метод: записывает в лог значение любого типа
    public void Log<T>(T value)
    {
        // вызываем событие, если на него кто-то подписан
        MessageLogged?.Invoke($"[{DateTime.Now:HH:mm:ss}] {value}");
    }

    // обобщённый метод: записывает в лог сообщение и связанные с ним данные
    public void Log<T>(string message, T data)
    {
        Log($"{message}: {data}");
    }
}
