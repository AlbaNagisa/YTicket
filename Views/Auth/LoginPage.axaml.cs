using Avalonia.Controls;
using Yticket.ViewModels.Auth;

namespace Yticket.Views.Auth
{
    public partial class LoginPage : UserControl
    {
        public LoginPage()
        {
            InitializeComponent();
            DataContext = new LoginPageViewModel();
        }

    
    }
}