using Aspid.UI.MVVM;
using Aspid.UI.MVVM.ViewModels;
using Aspid.UI.MVVM.ViewModels.Generation;

namespace MVVMGenerators.Sample.ViewModels;

public class ViewModel1 : IViewModel
{
    public void AddBinder(IBinder binder, string propertyName)
    {
        
    }

    public void RemoveBinder(IBinder binder, string propertyName)
    {
        
    }
}

[ViewModel]
public partial class ChildViewModel1 : ViewModel1
{
    
}

public interface IViewModel2 : IViewModel
{
    
}

[ViewModel]
public partial class ViewModel2 : IViewModel2
{
    
}