using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Yticket.Views;
using Yticket.Views.Auth;

namespace Yticket.Services;

public class PagePool
{
    private List<UserControl> pages = new();
    private static PagePool Instance;
    
    private PagePool()
    {
        pages.Add(new LoginPage());
        pages.Add(new MainPage());

    }
    
    public static PagePool GetInstance()
    {
        return Instance ??= new PagePool();
    }
    
    public UserControl GetPage(Type page)
    {
        foreach (UserControl item in pages)
        {
            if (item.GetType() == page)
            {
                return item;
            }
        }
        return null;
    }
}