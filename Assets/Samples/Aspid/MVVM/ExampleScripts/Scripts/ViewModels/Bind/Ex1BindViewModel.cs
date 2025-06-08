namespace Aspid.MVVM.ExampleScripts.ViewModels.Bind
{
    [ViewModel]
    public partial class Ex1BindViewModel
    {
        [Bind] private int _twoWayValue;
        [Bind] private readonly int _oneTimeValue;
    }
}