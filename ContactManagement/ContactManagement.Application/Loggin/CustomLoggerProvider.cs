using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;

namespace ContactManagement.Application.Loggin;

public class CustomLoggerProvider : ILoggerProvider
{
    private readonly CustomLoggerProviderConfiguration _loggerConfiguration;
    private readonly ConcurrentDictionary<string, CustomLogger> _loggers =
        new ConcurrentDictionary<string, CustomLogger>();

    public CustomLoggerProvider(CustomLoggerProviderConfiguration loggerConfiguration)
    {
        _loggerConfiguration = loggerConfiguration;
    }
    
    public ILogger CreateLogger(string categoryName)
    {
        return _loggers.GetOrAdd(categoryName, name => new CustomLogger(name, _loggerConfiguration));
    }
    public void Dispose()
    {
        throw new NotImplementedException();
    }
}