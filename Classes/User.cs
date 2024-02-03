using System;
using System.Text.Json.Serialization;

namespace Yticket.Models;

public class User
{
    [JsonPropertyName("id")]
    public int id { get; set; }

    [JsonPropertyName("name")]
    public string name { get; set; }

    [JsonPropertyName("email")]
    public string email { get; set; }

    [JsonPropertyName("email_verified_at")]
    public object email_verified_at { get; set; }

    [JsonPropertyName("created_at")]
    public DateTime created_at { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTime updated_at { get; set; }

    [JsonPropertyName("admin")]
    public bool admin { get; set; }
    
    
    [JsonPropertyName("authorization")]
    public Authorization authorization { get; set; }
}

public class Authorization
{
    [JsonPropertyName("access_token")]
    public string access_token { get; set; }

    [JsonPropertyName("token_type")]
    public string token_type { get; set; }

    [JsonPropertyName("expires_in")]
    public int expires_in { get; set; }
}
