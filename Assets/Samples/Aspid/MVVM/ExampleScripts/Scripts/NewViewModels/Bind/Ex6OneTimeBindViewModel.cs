namespace Aspid.MVVM.ExampleScripts.NewViewModels.Bind
{
    [ViewModel]
    public partial class Ex6OneTimeBindViewModel
    {
        // Альтернатива [Bind(BindMode.OneTime)]
        [OneTimeBind] private int _age;
    }
}