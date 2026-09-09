using PlaywrightSpecFlow.Tests.Pages.Base;
using PlaywrightSpecFlow.Tests.Support;

namespace PlaywrightSpecFlow.Tests.Pages;

public class LoginPage : BasePage
{
    private const string UsernameInput = "#user-name";
    private const string PasswordInput = "#password";
    private const string LoginButton = "#login-button";
    private const string ErrorMessage = "[data-test='error']";

    public LoginPage(PlaywrightDriver driver) : base(driver)
    {
    }

    public Task NavigateToAsync(string baseUrl) => NavigateAsync(baseUrl);

    public Task EnterUsernameAsync(string username) => FillAsync(UsernameInput, username);

    public Task EnterPasswordAsync(string password) => FillAsync(PasswordInput, password);

    public Task ClickLoginButtonAsync() => ClickAsync(LoginButton);

    public async Task LoginAsync(string username, string password)
    {
        await EnterUsernameAsync(username);
        await EnterPasswordAsync(password);
        await ClickLoginButtonAsync();
    }

    public Task<string> GetErrorMessageAsync() => GetTextAsync(ErrorMessage);

    public Task<bool> IsErrorMessageDisplayedAsync() => IsVisibleAsync(ErrorMessage);
}
