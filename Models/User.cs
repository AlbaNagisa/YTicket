using System;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Yticket.Models.Utils;

namespace Yticket.Models;

public class Authorization
{
    public string access_token { get; set; }
    public string token_type { get; set; }
    public int expires_in { get; set; }
}

public class Root
{
    [JsonPropertyName("user")]
    public User user { get; set; }
    [JsonPropertyName("authorization")]
    public Authorization authorization { get; set; }
}

public class User
{
    public int id { get; set; }
    public string name { get; set; }
    public string email { get; set; }
    public object email_verified_at { get; set; }
    public DateTime created_at { get; set; }
    public DateTime updated_at { get; set; }
    public bool admin { get; set; }
    public async Task<string> Verify(string email, string password)
    {
        try
        {
            return await Requests.GetInstance().Post("auth/login", new { email, password });
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine("Error: Resource not found (404).");

            return string.Empty;
        }
    }
}
    