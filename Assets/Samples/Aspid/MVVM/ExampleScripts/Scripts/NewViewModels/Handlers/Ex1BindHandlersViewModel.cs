namespace Aspid.MVVM.ExampleScripts.NewViewModels.Handlers
{
    [ViewModel]
    public partial class Ex1BindHandlersViewModel
    {
        [Bind] private string _name;

        partial void OnNameChanging(string oldValue, string newValue)
        {
            // Вызывается до изменения Name
        }

        partial void OnNameChanged(string newValue)
        {
            // Вызывается после изменения Name
        }
    }
}