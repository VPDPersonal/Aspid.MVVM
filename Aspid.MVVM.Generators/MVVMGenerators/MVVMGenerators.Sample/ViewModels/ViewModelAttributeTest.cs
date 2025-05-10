using Aspid.MVVM;

namespace MVVMGenerators.Sample.ViewModels;

[ViewModel]
public partial class ViewModelAttributeTest1
{
    [Bind] private int _bindAuto;
    [Bind] private readonly int _bindAutoReadonly;
    
    [Bind(BindMode.OneWay)] private int _bindOneWay;
    // [Bind(BindMode.OneWay)] private readonly int _bindOneWayReadonly; -> Don't work
    
    [Bind(BindMode.TwoWay)] private int _bindTwoWay;
    // [Bind(BindMode.TwoWay)] private readonly int _bindTwoWayReadonly; -> Don't work
    
    [Bind(BindMode.OneTime)] private int _bindOneTime;
    [Bind(BindMode.OneTime)] private readonly int _bindOneTimeReadonly;
    
    [Bind(BindMode.OneWayToSource)] private int _bindOneWayToSource;
    // [Bind(BindMode.OneWayToSource)] private readonly int _bindOneWayToSourceReadonly; -> Don't work

    [OneWayBind] private int _oneWayBind;
    // [OneWayBind] private readonly int _oneWayBindReadonly; -> Don't work
    
    [TwoWayBind] private int _twoWayBind;
    // [TwoWayBind] private readonly int _twoWayBindReadonly; -> Don't work
    
    [OneTimeBind] private int _oneTimeBind;
    [OneTimeBind] private readonly int _oneTimeBindReadonly;
    
    [OneWayToSourceBind] private int _oneWayToSourceBind;
    // [OneWayToSourceBind] private readonly int _oneWayToSourceBindReadonly; -> Don't work
}