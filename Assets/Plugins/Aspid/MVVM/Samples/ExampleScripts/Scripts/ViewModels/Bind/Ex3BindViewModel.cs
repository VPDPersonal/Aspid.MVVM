namespace Aspid.MVVM.Samples.ExampleScripts.ViewModels.Bind
{
    [ViewModel]
    public partial class Ex3BindViewModel
    {
        [OneTimeBind] private const int ConstOneTime = 0;
        
        [OneWayBind] private int _oneWayValue;
        [TwoWayBind] private int _twoWayValue;
        [OneTimeBind] private int _oneTimeValue1;
        [OneTimeBind] private readonly int _oneTimeValue2;
        [OneWayToSourceBind] private int _oneWayToSourceValue;
    }
}