using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Yticket.Helpers;

public class Requests
{
    private string _url;

    private void InitRequest(string url, HttpMethod method, string? bearer = null)
    {
        var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Get, "http://yticket-api.test/api/auth/me");
        request.Headers.Add("Accept", "application/json");

        if (bearer is not null)
        {
            request.Headers.Add("Authorization", "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJodHRwOi8veXRpY2tldC1hcGkudGVzdC9hcGkvYXV0aC9sb2dpbiIsImlhdCI6MTcwNjYxNDAwNywiZXhwIjoxNzA2NjE3NjA3LCJuYmYiOjE3MDY2MTQwMDcsImp0aSI6ImRmb2tCcVkweVlHMjgwa0kiLCJzdWIiOiI0IiwicHJ2IjoiMjNiZDVjODk0OWY2MDBhZGIzOWU3MDFjNDAwODcyZGI3YTU5NzZmNyJ9.4jO0dHD0acQFGQsiC8X-KIKAr1Z73KnolbpeIVTvN2g");
        }
    }
    
    public async Task Get(string url, string? bearer = null)
    {
        var content = new StringContent("{\n    \"name\" : \"mazbae\",\n    \"email\" : \"mrlog42@gmail.com\",\n    \"password\" : \"passworrrrd1234A\"\n}", null, "application/json");
        request.Content = content;
        var response = await client.SendAsync(request);
        response.EnsureSuccessStatusCode();
        Console.WriteLine(await response.Content.ReadAsStringAsync());

    }
}