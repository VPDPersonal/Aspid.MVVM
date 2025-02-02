using Aspid.MVVM;
using Aspid.MVVM.Generation;

namespace MVVMGenerators.Sample.ViewModels;

[ViewModel]
public partial class OneTimeBindViewModel1
{
    [OneTimeBind] private int _amount1;
    [Bind(BindMode.OneTime)] private int _amount2;
    
    [Bind] private readonly int _readonlyAmount3;
    [OneWayBind] private readonly int _readonlyAmount4;
    [OneTimeBind] private readonly int _readonlyAmount1;
    [Bind(BindMode.OneWay)] private readonly int _readonlyAmount5;
    [Bind(BindMode.OneTime)] private readonly int _readonlyAmount2;
}