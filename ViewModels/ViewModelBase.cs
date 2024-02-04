using System;
using System.ComponentModel;
using ReactiveUI;

namespace Yticket.ViewModels;

public class ViewModelBase : ReactiveObject
{
    public virtual void Enter()
    {
        Console.WriteLine("Enter in :" + this.GetType().Name);
    }

    public virtual void Exit()
    {
        Console.WriteLine("Exit :" + this.GetType().Name);
    }
}