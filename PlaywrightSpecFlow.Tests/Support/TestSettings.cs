namespace PlaywrightSpecFlow.Tests.Support;

public class TestSettings
{
    public string BaseUrl { get; set; } = string.Empty;
    public string Browser { get; set; } = "chromium";
    public bool Headless { get; set; } = true;
    public int DefaultTimeout { get; set; } = 30000;
    public float SlowMo { get; set; }
    public string ScreenshotsPath { get; set; } = "Screenshots";
}
