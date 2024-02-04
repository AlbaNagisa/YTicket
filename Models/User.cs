using System;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Yticket.Models.Utils;

namespace Yticket.Models;

public class User
{
    public int id { get; set; }
    public string name { get; set; }
    public string email { get; set; }
    public object email_verified_at { get; set; }
    public DateTime created_at { get; set; }
    public DateTime updated_at { get; set; }
    public bool admin { get; set; }

    public static User Instance { get; set; }

    private User()
    {
    }

    public static User GetInstance()
    {
        if (Instance == null)
        {
            Instance = new User();
        }

        return Instance;
    }


    public async Task<string> Verify(string email, string password)
    {
        try
        {
            var resJson = await Requests.GetInstance().Post("auth/login", new { email, password });
            var res = JsonConvert.DeserializeObject<Root>(resJson);
            Instance = res.user;
            return resJson;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine("Error: Resource not found (404).");

            return string.Empty;
        }
    }
}

public class Root
{
    [JsonPropertyName("user")] public User user { get; set; }
    [JsonPropertyName("authorization")] public Authorization authorization { get; set; }
}