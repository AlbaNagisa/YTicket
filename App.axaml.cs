using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Yticket.Models;
using Yticket.ViewModels;
using Yticket.Views;

namespace Yticket;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override async void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            Splash splashWindow = new Splash();
            SplashViewModel splashViewModel = new SplashViewModel();
            
            splashWindow.DataContext = splashViewModel;
            desktop.MainWindow = splashWindow;

            try
            {
           
                splashViewModel.StartUpMessage = "Loading Ressources...";
                await Task.Delay(100, cancellationToken: splashViewModel.CancellationToken);
                splashViewModel.StartUpMessage = "Get data from online...";
                await Task.Delay(100, cancellationToken: splashViewModel.CancellationToken);
                splashViewModel.StartUpMessage = ENV.TEST;
                await Task.Delay(100, cancellationToken: splashViewModel.CancellationToken);
            }
            catch (TaskCanceledException e)
            {
                splashWindow.Close();
                return;
            }

            var mainWindow = new MainWindow();
            desktop.MainWindow = mainWindow;
            mainWindow.Show();
            splashWindow.Close();
        }

        base.OnFrameworkInitializationCompleted();
    }
}