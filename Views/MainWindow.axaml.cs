using Avalonia.Controls;
using Yticket.Services;

namespace Yticket.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = Navigation.GetInstance();
    }
}