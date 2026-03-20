using Microsoft.Playwright;

namespace QaAutomationAssignment.Pages;

public class DynamicLoadingPage
{
    private readonly IPage _page;

    public DynamicLoadingPage(IPage page)
    {
        _page = page;
    }

    public async Task ClickStartAsync()
    {
        await _page.GetByRole(AriaRole.Button, new() { Name = "Start" }).ClickAsync();
    }

    public async Task<string> GetLoadedTextAsync()
    {
        var result = _page.Locator("#finish h4");
        await result.WaitForAsync(new LocatorWaitForOptions
        {
            State = WaitForSelectorState.Visible
        });

        return (await result.TextContentAsync())?.Trim() ?? string.Empty;
    }
}