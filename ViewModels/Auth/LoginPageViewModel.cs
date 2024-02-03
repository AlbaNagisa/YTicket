using System;
using System.Reactive;
using System.Reactive.Linq;
using ReactiveUI;
using Yticket.Models;
using Newtonsoft.Json;
using Yticket.Services;
using Yticket.Views;

namespace Yticket.ViewModels.Auth;



public class LoginPageViewModel : ReactiveObject
{
    private string _email;
    private string _password;
    private string _errorMessage;
    private string _result;

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

    public string Result
    {
        get => _result;
        set => this.RaiseAndSetIfChanged(ref _result, value);
    }

    public ReactiveCommand<Unit, string> Login { get; }

    public LoginPageViewModel()
    {
        Login = ReactiveCommand.CreateFromObservable(() => LoginAsync());

        // Subscribe to the command's output
        Login.Subscribe(result =>
        {
            try
            {
                Root Result = (Root)JsonConvert.DeserializeObject(result, typeof(Root));
                Console.WriteLine(Result.user.name);
                Navigation.GetInstance().NavigateToPage(new SecondPage());
                ErrorMessage = string.Empty;
            } catch (Exception ex)
            {
                ErrorMessage = $"Identifiant ou mot de passe incorrect";
            }
            
        });
    }


    private IObservable<string> LoginAsync()
    {
        try
        {
            return Observable.FromAsync(() => new User().Verify(Email, Password));
        }
        catch (Exception ex)
        {
            // Handle exceptions
            ErrorMessage = $"Login error: {ex.Message}";
            return Observable.Empty<string>(); // or Observable.Throw<string>(ex) if you want to propagate the error
        }
    }
}