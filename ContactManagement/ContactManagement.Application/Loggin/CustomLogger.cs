using System.Globalization;
using Microsoft.Extensions.Logging;

namespace ContactManagement.Application.Loggin;

public class CustomLogger : ILogger
{
    private readonly string _loggerName;
    private readonly CustomLoggerProviderConfiguration _loggerConfig;
    public static bool ToFile { get; set; } = true;
    public CustomLogger(string loggerName, CustomLoggerProviderConfiguration loggerConfig)
    {
        _loggerName = loggerName;
        _loggerConfig = loggerConfig;
    }
    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        return null;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return true;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
       var message = $"Execution Log for {logLevel}: {eventId} {formatter(state, exception)}";
       if (ToFile)
       {
           WriteLogInFile(message);
       }
       else
       {
           Console.WriteLine(message);
       }
       
    }

    private void WriteLogInFile(string message)
    {
        var filePath = Environment.CurrentDirectory + @$"/LOG-{DateTime.Now:yyyy-MM-dd}.txt";
        if (!File.Exists(filePath))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            File.Create(filePath).Dispose();
        }
        using StreamWriter sw = new(filePath, true);
        sw.WriteLine($"[{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture)}] - {message}");
        sw.Close();
    }
}