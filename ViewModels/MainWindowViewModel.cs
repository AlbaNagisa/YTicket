using ReactiveUI;

namespace Yticket.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private string _greeting = "Hello World!";
    public string Greeting
    {
        get => _greeting;
        set => this.RaiseAndSetIfChanged(ref _greeting, value);
    }
}