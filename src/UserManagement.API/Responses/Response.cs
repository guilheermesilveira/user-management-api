using System.Text.Json.Serialization;

namespace UserManagement.API.Responses;

public abstract class Response
{
    [JsonPropertyOrder(order: 1)]
    public string Title { get; set; } = null!;
    
    [JsonPropertyOrder(order: 2)]
    public int Status { get; set; }
}