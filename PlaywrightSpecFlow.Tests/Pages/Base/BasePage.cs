using Microsoft.Playwright;
using PlaywrightSpecFlow.Tests.Support;

namespace PlaywrightSpecFlow.Tests.Pages.Base;

/// <summary>
/// Base class for all page objects. Exposes the shared Playwright page and a
/// small set of common actions so concrete pages only need to define their
/// own locators and business-facing methods.
/// </summary>
public abstract class BasePage
{
    protected readonly IPage Page;

    protected BasePage(PlaywrightDriver driver)
    {
        Page = driver.Page ?? throw new InvalidOperationException(
            "Page is not initialized. Ensure BeforeScenario hook has run before resolving page objects.");
    }

    protected Task NavigateAsync(string url) => Page.GotoAsync(url);

    protected Task ClickAsync(string selector) => Page.ClickAsync(selector);

    protected Task FillAsync(string selector, string text) => Page.FillAsync(selector, text);

    protected async Task<string> GetTextAsync(string selector) => await Page.InnerTextAsync(selector);

    protected Task<bool> IsVisibleAsync(string selector) => Page.IsVisibleAsync(selector);

    protected async Task WaitForSelectorAsync(string selector) => await Page.WaitForSelectorAsync(selector);

    public Task<string> GetTitleAsync() => Page.TitleAsync();

    public string GetUrl() => Page.Url;
}
