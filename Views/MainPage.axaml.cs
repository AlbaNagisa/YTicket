using Avalonia.Controls;
using Avalonia.Input;
using Yticket.ViewModels;

namespace Yticket.Views;

public partial class MainPage : UserControl
{
    public MainPage()
    {
        InitializeComponent();
        DataContext = new MainPageViewModel();
    }
}