using System;
using UltimateUI.MVVM;
using UltimateUI.MVVM.ViewModels;

namespace MVVMGenerators.Sample;

public partial class SomeBinder : SomeBinderParent, IBinder<string>, IBinder<object>
{
    [BinderLog]
    public void SetValue(string value)
    {
        throw new NotImplementedException();
    }

    [BindInheritorsAlso]
    public void SetValue(object value)
    {
        
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

    [BindInheritorsAlso]
    public void SetValue(IViewModel value)
    {
        
    }
}

public interface INumberBinder : IBinder<long>, IBinder<int> { }