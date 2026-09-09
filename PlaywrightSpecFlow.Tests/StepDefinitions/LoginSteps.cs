using PlaywrightSpecFlow.Tests.Pages;
using PlaywrightSpecFlow.Tests.Support;
using TechTalk.SpecFlow;

namespace PlaywrightSpecFlow.Tests.StepDefinitions;

[Binding]
public class LoginSteps
{
    private readonly LoginPage _loginPage;
    private readonly DashboardPage _dashboardPage;

    public LoginSteps(LoginPage loginPage, DashboardPage dashboardPage)
    {
        _loginPage = loginPage;
        _dashboardPage = dashboardPage;
    }

    [Given(@"I am on the login page")]
    public async Task GivenIAmOnTheLoginPage()
    {
        await _loginPage.NavigateToAsync(ConfigReader.Settings.BaseUrl);
    }

    [When(@"I enter username ""(.*)"" and password ""(.*)""")]
    public async Task WhenIEnterUsernameAndPassword(string username, string password)
    {
        await _loginPage.EnterUsernameAsync(username);
        await _loginPage.EnterPasswordAsync(password);
    }

    [When(@"I click the login button")]
    public async Task WhenIClickTheLoginButton()
    {
        await _loginPage.ClickLoginButtonAsync();
    }

    [Then(@"I should be navigated to the dashboard page")]
    public async Task ThenIShouldBeNavigatedToTheDashboardPage()
    {
        var isDisplayed = await _dashboardPage.IsDisplayedAsync();
        Assert.That(isDisplayed, Is.True, "Dashboard page was not displayed after login.");
    }

    [Then(@"I should see an error message containing ""(.*)""")]
    public async Task ThenIShouldSeeAnErrorMessageContaining(string expectedMessage)
    {
        var actualMessage = await _loginPage.GetErrorMessageAsync();
        Assert.That(actualMessage, Does.Contain(expectedMessage));
    }
}
