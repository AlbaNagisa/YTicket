using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Yticket.Services;
using Yticket.ViewModels;
using Yticket.ViewModels.Auth;
using Yticket.Views.Auth;
namespace Yticket.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = Navigation.GetInstance();
        
        Navigation.GetInstance().NavigateToPage(new LoginPage());
    }
}