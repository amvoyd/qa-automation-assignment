using System.Text.Json.Serialization;

namespace QaAutomationAssignment.Models;

public class ApiObject
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }
}