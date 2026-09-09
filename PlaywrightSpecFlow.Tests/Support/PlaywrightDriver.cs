using Microsoft.Playwright;

namespace PlaywrightSpecFlow.Tests.Support;

/// <summary>
/// Owns the Playwright/browser/context/page lifecycle for a single scenario.
/// Created and disposed by <see cref="Hooks.Hooks"/>; resolved via SpecFlow's
/// context injection so page objects and steps share the same page instance.
/// </summary>
public sealed class PlaywrightDriver : IAsyncDisposable
{
    public IPlaywright? Playwright { get; private set; }
    public IBrowser? Browser { get; private set; }
    public IBrowserContext? Context { get; private set; }
    public IPage? Page { get; private set; }

    public async Task InitializeAsync()
    {
        var settings = ConfigReader.Settings;

        Playwright = await Microsoft.Playwright.Playwright.CreateAsync();
        Browser = await LaunchBrowserAsync(settings);
        Context = await Browser.NewContextAsync(new BrowserNewContextOptions
        {
            ViewportSize = new ViewportSize { Width = 1920, Height = 1080 }
        });
        Context.SetDefaultTimeout(settings.DefaultTimeout);
        Page = await Context.NewPageAsync();
    }

    private async Task<IBrowser> LaunchBrowserAsync(TestSettings settings)
    {
        if (Playwright is null)
        {
            throw new InvalidOperationException("Playwright must be created before launching a browser.");
        }

        var launchOptions = new BrowserTypeLaunchOptions
        {
            Headless = settings.Headless,
            SlowMo = settings.SlowMo
        };

        return settings.Browser.ToLowerInvariant() switch
        {
            "firefox" => await Playwright.Firefox.LaunchAsync(launchOptions),
            "webkit" => await Playwright.Webkit.LaunchAsync(launchOptions),
            _ => await Playwright.Chromium.LaunchAsync(launchOptions)
        };
    }

    public async ValueTask DisposeAsync()
    {
        if (Context is not null)
        {
            await Context.CloseAsync();
        }

        if (Browser is not null)
        {
            await Browser.CloseAsync();
        }

        Playwright?.Dispose();
    }
}
