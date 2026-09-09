using Microsoft.Playwright;
using PlaywrightSpecFlow.Tests.Support;

namespace PlaywrightSpecFlow.Tests.Utilities;

public static class ScreenshotHelper
{
    public static async Task<string?> CaptureAsync(IPage? page, string scenarioTitle)
    {
        if (page is null)
        {
            return null;
        }

        var screenshotsDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigReader.Settings.ScreenshotsPath);
        Directory.CreateDirectory(screenshotsDir);

        var invalidChars = Path.GetInvalidFileNameChars();
        var safeName = new string(scenarioTitle.Select(c => invalidChars.Contains(c) ? '_' : c).ToArray());
        var fileName = $"{safeName}_{DateTime.Now:yyyyMMdd_HHmmss}.png";
        var filePath = Path.Combine(screenshotsDir, fileName);

        await page.ScreenshotAsync(new PageScreenshotOptions { Path = filePath, FullPage = true });

        TestContext.AddTestAttachment(filePath, $"Screenshot: {scenarioTitle}");

        return filePath;
    }
}
