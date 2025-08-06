namespace Aspid.MVVM.Samples.ExampleScripts.ViewModels.Bind
{
    [ViewModel]
    public partial class Ex2BindViewModel
    {
        [Bind(BindMode.OneTime)] private const int ConstOneTime = 0;
        
        [Bind(BindMode.OneWay)] private int _oneWayValue;
        [Bind(BindMode.TwoWay)] private int _twoWayValue;
        [Bind(BindMode.OneTime)] private int _oneTimeValue1;
        [Bind(BindMode.OneTime)] private readonly int _oneTimeValue2;
        [Bind(BindMode.OneWayToSource)] private int _oneWayToSourceValue;
    }
}