using Microsoft.Extensions.Configuration;

namespace PlaywrightSpecFlow.Tests.Support;

public static class ConfigReader
{
    private static readonly Lazy<TestSettings> LazySettings = new(LoadSettings);

    public static TestSettings Settings => LazySettings.Value;

    private static TestSettings LoadSettings()
    {
        var environment = Environment.GetEnvironmentVariable("TEST_ENVIRONMENT") ?? "dev";
        var configDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config");

        var configuration = new ConfigurationBuilder()
            .SetBasePath(configDir)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
            .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: false)
            .AddEnvironmentVariables()
            .Build();

        var settings = new TestSettings();
        configuration.Bind(settings);
        return settings;
    }
}
