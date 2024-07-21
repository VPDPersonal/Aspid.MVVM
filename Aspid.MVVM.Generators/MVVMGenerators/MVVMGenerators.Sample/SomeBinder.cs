using System;
using UltimateUI.MVVM;
using UltimateUI.MVVM.Unity;
using UltimateUI.MVVM.ViewModels;

namespace MVVMGenerators.Sample;

public partial class SomeBinder : SomeBinderParent, IBinder<string>, IBinder<object>, IBinder<IBinder>
{
    [BinderLog]
    public void SetValue(string value)
    {
        throw new NotImplementedException();
    }

    [BinderLog]
    void IBinder<object>.SetValue(object value)
    {
        throw new NotImplementedException();
    }

    public void SetValue(IBinder value)
    {
        throw new NotImplementedException();
    }
}

public partial class SomeBinderParent : INumberBinder, IBinder<IViewModel>
{
    [BinderLog]
    public void SetValue(int value)
    {
    }
    
    [BinderLog]
    public void SetValue(long value)
    {
        
    }

    [BinderLog]
    public void SetValue(IViewModel value)
    {
        throw new NotImplementedException();
    }
    
    public void Bind(IViewModel viewModel, string id)
    {
        throw new NotImplementedException();
    }
    
    public void Unbind(IViewModel viewModel, string id)
    {
        throw new NotImplementedException();
    }
}

public interface INumberBinder : IBinder<long>, IBinder<int> { }