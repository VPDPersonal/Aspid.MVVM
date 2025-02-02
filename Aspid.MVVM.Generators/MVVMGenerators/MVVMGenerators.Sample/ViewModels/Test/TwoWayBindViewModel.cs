using Aspid.MVVM;
using Aspid.MVVM.Generation;

namespace MVVMGenerators.Sample.ViewModels;

[ViewModel]
public partial class TwoWayBindViewModel
{
    [Bind] private int _amount1;
    [TwoWayBind] private int _amount2;
    [Bind(BindMode.TwoWay)] private int _amount3;
    
    // Work as OneTime
    [Bind] private readonly int _readonlyAmount1;
    
    // Don't work
    [TwoWayBind] private readonly int _readonlyAmount2;
    [Bind(BindMode.TwoWay)] private readonly int _readonlyAmount3;
}