namespace Aspid.MVVM.ExampleScripts.NewViewModels.Bind
{
    [ViewModel]
    public partial class Ex4OneWayBindViewModel
    {
        // Альтернатива [Bind(BindMode.OneWay)]
        [OneWayBind] private int _age;
    }
}