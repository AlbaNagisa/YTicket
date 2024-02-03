using System.Threading;
using ReactiveUI;

namespace Yticket.ViewModels;

internal class SplashViewModel : ViewModelBase
{
    private string _startUpMessage = "Starting up...";
    public string StartUpMessage 
    {
        get => _startUpMessage;
        set => this.RaiseAndSetIfChanged(ref _startUpMessage, value);
    }

    private CancellationTokenSource _cts = new CancellationTokenSource();
    public CancellationToken CancellationToken => _cts.Token;

    public void Cancel()
    {
        _cts.Cancel();
    }
    
}