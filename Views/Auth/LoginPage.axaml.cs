using System;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ReactiveUI;
using Yticket.ViewModels.Auth;

namespace Yticket.Views.Auth
{
    public partial class LoginPage : UserControl
    {
        public LoginPage()
        {
            InitializeComponent();
            this.DataContext = new LoginPageViewModel();
        }

    
    }
}