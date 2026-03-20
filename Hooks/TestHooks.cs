using Microsoft.Playwright;
using Reqnroll;

namespace QaAutomationAssignment.Hooks;

[Binding]
public class TestHooks
{
    private readonly ScenarioContext _scenarioContext;
    private IPlaywright _playwright = null!;
    private IBrowser _browser = null!;
    private IBrowserContext _browserContext = null!;
    private IPage _page = null!;

    public TestHooks(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
    }

    [BeforeScenario]
    public async Task BeforeScenario()
    {
        _playwright = await Playwright.CreateAsync();
        _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = true
        });

        _browserContext = await _browser.NewContextAsync(new BrowserNewContextOptions
        {
            IgnoreHTTPSErrors = true
        });

        _page = await _browserContext.NewPageAsync();

        _scenarioContext["Page"] = _page;
        _scenarioContext["BrowserContext"] = _browserContext;
    }

    [AfterScenario]
    public async Task AfterScenario()
    {
        if (_scenarioContext.TestError != null)
        {
            var page = (IPage)_scenarioContext["Page"];
            Directory.CreateDirectory("artifacts");
            await page.ScreenshotAsync(new PageScreenshotOptions
            {
                Path = $"artifacts/failed-{DateTime.Now:yyyyMMdd-HHmmss}.png",
                FullPage = true
            });
        }

        await _browserContext.CloseAsync();
        await _browser.CloseAsync();
        _playwright.Dispose();
    }
}