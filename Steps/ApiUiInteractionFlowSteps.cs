using Microsoft.Playwright;
using NUnit.Framework;
using QaAutomationAssignment.Helpers;
using QaAutomationAssignment.Pages;
using QaAutomationAssignment.Services;
using Reqnroll;

namespace QaAutomationAssignment.Steps;

[Binding]
public class ApiUiInteractionFlowSteps
{
    private readonly ScenarioContext _scenarioContext;
    private readonly TestContextData _testData;
    private readonly IPage _page;

    private readonly ApiClient _apiClient;
    private readonly HomePage _homePage;
    private readonly DynamicLoadingPage _dynamicLoadingPage;
    private readonly FormAuthenticationPage _formAuthenticationPage;

    public ApiUiInteractionFlowSteps(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
        _page = (IPage)_scenarioContext["Page"];

        _testData = _scenarioContext.ContainsKey("TestData")
            ? (TestContextData)_scenarioContext["TestData"]
            : new TestContextData();

        _scenarioContext["TestData"] = _testData;

        _apiClient = new ApiClient();
        _homePage = new HomePage(_page);
        _dynamicLoadingPage = new DynamicLoadingPage(_page);
        _formAuthenticationPage = new FormAuthenticationPage(_page);
    }

    [Given(@"a GET request to ""(.*)"" is sent")]
    public async Task SendGetRequest(string url)
    {
        var objects = await _apiClient.GetObjectsAsync(url);

        Assert.That(objects, Is.Not.Empty, "API response returned no objects.");
        Assert.That(objects[0].Name, Is.Not.Null.And.Not.Empty, "First object name is empty.");

        _testData.FirstObjectName = objects[0].Name!;
        Console.WriteLine($"First object name from API: {_testData.FirstObjectName}");
    }

    [Given(@"the user navigates to ""(.*)""")]
    public async Task NavigateTo(string url)
    {
        await _homePage.NavigateAsync(url);
    }

    [Given(@"the user opens Slow Resources and logs the response")]
    public async Task OpenSlowResources()
    {
        var responseTask = _page.WaitForResponseAsync(response => response.Url.Contains("slow"));
        await _homePage.OpenSlowResourcesAsync();
        var response = await responseTask;

        Console.WriteLine($"Slow Resources response status: {response.Status}");
        Assert.That(response.Ok, Is.True, "Slow Resources response was not successful.");
    }

    [Given(@"the user opens Dynamic Loading Example 1")]
    public async Task OpenDynamicLoading()
    {
        await _homePage.OpenDynamicLoadingAsync();
    }

    [Given(@"the user clicks the Start button")]
    public async Task  ClickStart()
    {
        await _dynamicLoadingPage.ClickStartAsync();
    }

    [Given(@"the user saves the loaded result text after loading")]
    public async Task SaveLoadedText()
    {
        _testData.DynamicLoadingResultText = await _dynamicLoadingPage.GetLoadedTextAsync();

        Assert.That(_testData.DynamicLoadingResultText, Is.Not.Empty, "Loaded result text is empty.");
        Console.WriteLine($"Dynamic loading result text: {_testData.DynamicLoadingResultText}");
    }

    [When(@"the user opens Form Authentication")]
    public async Task OpenLoginPage()
    {
        await _homePage.OpenFormAuthenticationAsync();
    }

    [When(@"the user inputs the loaded result text as username")]
    public async Task EnterUsername()
    {
        await _page.Locator("#username").FillAsync(_testData.DynamicLoadingResultText);
    }

    [When(@"the user inputs the first object name from the API response as password")]
    public async Task EnterPassword()
    {
        await _page.Locator("#password").FillAsync(_testData.FirstObjectName);
    }

    [When(@"the user clicks on the Login button")]
    public async Task ClickLogin()
    {
        await _page.GetByRole(AriaRole.Button, new() { Name = "Login" }).ClickAsync();
    }

    [Then(@"the error message ""(.*)"" should appear")]
    public async Task VerifyErrorMessage(string expectedMessage)
    {
        var actualMessage = await _formAuthenticationPage.GetErrorMessageAsync();
        Assert.That(actualMessage, Does.Contain(expectedMessage));
    }
}