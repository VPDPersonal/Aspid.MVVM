namespace Aspid.MVVM.ExampleScripts.NewViewModels.Bind
{
    [ViewModel]
    public partial class Ex7OneWayToSourceBindViewModel
    {
        // Альтернатива [Bind(BindMode.OneWayToSource)]
        [OneWayToSourceBind] private int _age;
    }
}