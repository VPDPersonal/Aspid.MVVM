namespace Aspid.MVVM.ExampleScripts.ViewModels
{
    [ViewModel]
    public partial class Ex1BindViewModel
    {
        [Bind] private const int ConstOneTime = 0;
        
        [Bind] private int _twoWayValue;
        [Bind] private readonly int _oneTimeValue;
    }
}