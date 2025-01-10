using ContactManagement.Application.Loggin;

namespace ContactManagement.Api.Configurations;

public static class LoggingConfig
{
    public static WebApplicationBuilder AddCustomLogging(this WebApplicationBuilder builder)
    {
        builder.Logging.ClearProviders();
        builder.Logging.AddProvider(new CustomLoggerProvider(new CustomLoggerProviderConfiguration
        {
            LogLevel = LogLevel.Information,
        }));

        return builder;
    }
}

