using Microsoft.Playwright;

namespace QaAutomationAssignment.Pages;

public class FormAuthenticationPage
{
    private readonly IPage _page;

    public FormAuthenticationPage(IPage page)
    {
        _page = page;
    }

    public async Task LoginAsync(string username, string password)
    {
        await _page.Locator("#username").FillAsync(username);
        await _page.Locator("#password").FillAsync(password);
        await _page.GetByRole(AriaRole.Button, new() { Name = "Login" }).ClickAsync();
    }

    public async Task<string> GetErrorMessageAsync()
    {
        var message = _page.Locator("#flash");
        await message.WaitForAsync(new LocatorWaitForOptions
        {
            State = WaitForSelectorState.Visible
        });

        var text = (await message.TextContentAsync())?.Trim() ?? string.Empty;

        return text.Replace("×", "").Trim();
    }
}