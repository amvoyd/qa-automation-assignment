using Microsoft.Playwright;

namespace QaAutomationAssignment.Pages;

public class HomePage
{
    private readonly IPage _page;

    public HomePage(IPage page)
    {
        _page = page;
    }

    public async Task NavigateAsync(string url)
    {
        await _page.GotoAsync(url);
    }

    public async Task OpenSlowResourcesAsync()
    {
        await _page.GetByRole(AriaRole.Link, new() { Name = "Slow Resources" }).ClickAsync();
    }

    public async Task OpenDynamicLoadingAsync()
    {
        await _page.GotoAsync("https://the-internet.herokuapp.com/dynamic_loading");
        await _page.GetByRole(AriaRole.Link, new() { Name = "Example 1: Element on page that is hidden" }).ClickAsync();
    }

    public async Task OpenFormAuthenticationAsync()
    {
        await _page.GotoAsync("https://the-internet.herokuapp.com/login");
    }
}