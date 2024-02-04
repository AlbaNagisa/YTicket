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
        NavigateToPage(PagePool.GetInstance().GetPage(typeof(LoginPage)));
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
            _currentPage = value;
            OnPropertyChanged(nameof(CurrentPage));
        }
    }

    public void NavigateToPage(UserControl page)
    {
        if (CurrentPage != null)
        {
            CurrentPage.DataContext.GetType().GetMethod("Exit")?.Invoke(CurrentPage.DataContext, null);
        }

        CurrentPage = page;
        CurrentPage.DataContext.GetType().GetMethod("Enter")?.Invoke(CurrentPage.DataContext, null);
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}