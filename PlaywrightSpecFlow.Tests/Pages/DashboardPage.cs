using PlaywrightSpecFlow.Tests.Pages.Base;
using PlaywrightSpecFlow.Tests.Pages.Components;
using PlaywrightSpecFlow.Tests.Support;

namespace PlaywrightSpecFlow.Tests.Pages;

public class DashboardPage : BasePage
{
    private const string InventoryList = ".inventory_list";

    public HeaderComponent Header { get; }

    public DashboardPage(PlaywrightDriver driver) : base(driver)
    {
        Header = new HeaderComponent(Page);
    }

    public Task<bool> IsDisplayedAsync() => IsVisibleAsync(InventoryList);
}
