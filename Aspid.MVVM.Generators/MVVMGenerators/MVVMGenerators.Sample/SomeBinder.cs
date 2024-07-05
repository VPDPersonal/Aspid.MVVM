using System;
using UltimateUI.MVVM;

namespace MVVMGenerators.Sample;

public partial class SomeBinder : SomeBinderParent, IBinder<string>
{
    [BinderLog]
    public void SetValue(string value)
    {
        throw new NotImplementedException();
    }
}

public partial class SomeBinderParent : INumberBinder
{
    [BinderLog]
    public void SetValue(int value)
    {
    }

    // [BindInheritorsAlso]
    // public void SetValue(long value)
    // {
    //     
    // }
    
    [BinderLog]
    [BindInheritorsAlso]
    public void SetValue(long value)
    {
        
    }
}

public interface INumberBinder : IBinder<long>, IBinder<int> { }