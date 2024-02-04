using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Yticket.Models;

namespace Yticket.Models.Utils;

public class Requests
{
    private static Requests Instance;
    private static HttpClient Client;

    public static Requests GetInstance()
    {
        return Instance ??= new Requests();
    }

    private Requests()
    {
        Client = new HttpClient
        {
            BaseAddress = new Uri(ENV.API_URL),
            Timeout = TimeSpan.FromSeconds(10)
        };
        Client.DefaultRequestHeaders.Accept.Add(
            new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<string> Post(string url, object? body = null, string? bearer = null)
    {
        try
        {
            var content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");
            if (bearer is not null)
            {
                Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", bearer);
            }
            var response = await Client.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
            Console.WriteLine("POST request sent!");
            return await response.Content.ReadAsStringAsync();
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<string> Patch(string url, object? body = null, string? bearer = null)
    {
        try
        {
            var content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");
            if (bearer is not null)
            {
                Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", bearer);
            }
            var response = await Client.PatchAsync(url, content);
            response.EnsureSuccessStatusCode();
            Console.WriteLine("PATCH request sent!");
            return await response.Content.ReadAsStringAsync();
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    public async Task<string> Get(string url, string? bearer = null)
    {
        try
        {
            if (bearer is not null)
            {
                Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", bearer);
            }

            var response = await Client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}