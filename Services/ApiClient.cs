using System.Text.Json;
using QaAutomationAssignment.Models;

namespace QaAutomationAssignment.Services;

public class ApiClient
{
    private readonly HttpClient _httpClient = new();

    public async Task<List<ApiObject>> GetObjectsAsync(string url)
    {
        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var result = JsonSerializer.Deserialize<List<ApiObject>>(json, options);

        return result ?? new List<ApiObject>();
    }
}