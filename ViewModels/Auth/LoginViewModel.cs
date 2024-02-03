using System;
using System.Net.Http;
using System.Threading.Tasks;
using ReactiveUI;

namespace Yticket.ViewModels.Auth;

public class LoginViewModel : ViewModelBase
{
    private string _email = "";

    private string _password  = "";
    
    private string _errorMessage = "";

    public string Email
    {
        get => _email;
        set => this.RaiseAndSetIfChanged(ref _email, value);
    }
    
    public string Password
    {
        get => _password;
        set => this.RaiseAndSetIfChanged(ref _password, value);
    }
    
    public string ErrorMessage 
    {
        get => _errorMessage;
        set => this.RaiseAndSetIfChanged(ref _errorMessage, value);
    }
    
    public void Login()
    {
        LoginUser();
        ErrorMessage = "Btn clicked ! " + Email + " " + Password;
    }


    static async Task LoginUser()
    {
        var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Post, "http://yticket-api.test/api/auth/login");
        request.Headers.Add("Accept", "application/json");
        var content = new StringContent("{\n    \"email\" : \"mrlog42@gmail.com\",\n    \"password\" : \"passworrrrd1234A\"\n}", null, "application/json");
        request.Content = content;
        var response = await client.SendAsync(request);
        response.EnsureSuccessStatusCode();
        Console.WriteLine(await response.Content.ReadAsStringAsync());

    }
}