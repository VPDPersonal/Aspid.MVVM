namespace Aspid.MVVM.Samples.ExampleScripts.ViewModels.Bind
{
    [ViewModel]
    public partial class Ex1BindViewModel
    {
        [Bind] private const int ConstOneTime = 0;
        
        [Bind] private int _twoWayValue;
        [Bind] private readonly int _oneTimeValue;
    }
}