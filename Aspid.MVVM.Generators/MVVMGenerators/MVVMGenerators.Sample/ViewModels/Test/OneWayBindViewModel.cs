using Aspid.MVVM;
using Aspid.MVVM.Generation;

namespace MVVMGenerators.Sample.ViewModels;

[ViewModel]
public partial class OneWayBindViewModel
{
    [OneWayBind] private int _amount1;
    [Bind(BindMode.OneWay)] private int _amount2;

    // Work as OneTime
    [Bind] private readonly int _readonlyAmount3;
    [OneWayBind] private readonly int _readonlyAmount1;
    [Bind(BindMode.OneWay)] private readonly int _readonlyAmount2;
}
