using PlaywrightSpecFlow.Tests.Support;
using PlaywrightSpecFlow.Tests.Utilities;
using TechTalk.SpecFlow;

namespace PlaywrightSpecFlow.Tests.Hooks;

[Binding]
public sealed class Hooks
{
    private readonly PlaywrightDriver _driver;
    private readonly ScenarioContext _scenarioContext;

    public Hooks(PlaywrightDriver driver, ScenarioContext scenarioContext)
    {
        _driver = driver;
        _scenarioContext = scenarioContext;
    }

    [BeforeScenario]
    public async Task BeforeScenario()
    {
        await _driver.InitializeAsync();
    }

    [AfterScenario]
    public async Task AfterScenario()
    {
        if (_scenarioContext.TestError is not null)
        {
            await ScreenshotHelper.CaptureAsync(_driver.Page, _scenarioContext.ScenarioInfo.Title);
        }

        await _driver.DisposeAsync();
    }
}
