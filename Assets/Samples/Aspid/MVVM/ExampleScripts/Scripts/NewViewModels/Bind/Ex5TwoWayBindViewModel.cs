namespace Aspid.MVVM.ExampleScripts.NewViewModels.Bind
{
    [ViewModel]
    public partial class Ex5TwoWayBindViewModel
    {
        // Альтернатива [Bind(BindMode.TwoWay)]
        [TwoWayBind] private int _age;
    }
}