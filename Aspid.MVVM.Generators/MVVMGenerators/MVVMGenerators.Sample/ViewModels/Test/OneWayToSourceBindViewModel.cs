using Aspid.MVVM;
using Aspid.MVVM.Generation;

namespace MVVMGenerators.Sample.ViewModels;

[ViewModel]
public partial class OneWayToSourceBindViewModel1
{
    [OneWayToSourceBind] private int _amount1;
    [Bind(BindMode.OneWayToSource)] private int _amount2;
    
    // Don't work
    [OneWayToSourceBind] private readonly int _readonlyAmount1;
    [Bind(BindMode.OneWayToSource)] private readonly int _readonlyAmount2;
}