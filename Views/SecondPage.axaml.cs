using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Yticket.ViewModels;

namespace Yticket.Views;

public partial class SecondPage : UserControl
{
    public SecondPage()
    {
        InitializeComponent();
        DataContext = new SecondPageViewModel();
        Console.Write("second screen charged");

    }
}