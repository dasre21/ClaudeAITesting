using Microsoft.Playwright;

namespace PlaywrightSpecFlow.Tests.Pages.Components;

/// <summary>
/// Example reusable component that can be composed into multiple page objects
/// (e.g. a header/nav bar shown on every authenticated page).
/// </summary>
public class HeaderComponent
{
    private const string BurgerMenuButton = "#react-burger-menu-btn";
    private const string LogoutLink = "#logout_sidebar_link";
    private const string CartBadge = ".shopping_cart_badge";

    private readonly IPage _page;

    public HeaderComponent(IPage page)
    {
        _page = page;
    }

    public Task OpenMenuAsync() => _page.ClickAsync(BurgerMenuButton);

    public Task LogoutAsync() => _page.ClickAsync(LogoutLink);

    public async Task<int> GetCartItemCountAsync()
    {
        if (!await _page.IsVisibleAsync(CartBadge))
        {
            return 0;
        }

        var text = await _page.InnerTextAsync(CartBadge);
        return int.Parse(text);
    }
}
