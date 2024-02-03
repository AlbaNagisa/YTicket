using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Avalonia.Controls;
using ReactiveUI;
using Yticket.Views.Auth;

namespace Yticket.Services;

public class Navigation : ReactiveObject, INotifyPropertyChanged
{
    private UserControl _currentPage;

    public static Navigation Instance;

    private Navigation()
    {
        Console.WriteLine("Navigation created");
        CurrentPage = new LoginPage();
    }

    public static Navigation GetInstance()
    {
        return Instance ??= new Navigation();
    }

    public UserControl CurrentPage
    {
        get => _currentPage;
        set
        {
            if (_currentPage != value)
            {
                _currentPage = value;
                OnPropertyChanged(nameof(CurrentPage));
            }
        }
    }

    public void NavigateToPage(UserControl page)
    {
        CurrentPage = page;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}