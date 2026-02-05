// ReSharper disable once CheckNamespace
// ReSharper disable UnusedParameterInPartialMethod
// ReSharper disable RedundantPartialMethodEmptyImplementation
namespace Aspid.MVVM.Samples.ExampleScripts.ViewModels.Handlers
{
    [ViewModel]
    public partial class Ex1BindHandlersViewModel
    {
        [Bind] private string _name;

        partial void OnNameChanging(string oldValue, string newValue)
        {
            // Called before Name is changed
        }

        partial void OnNameChanged(string newValue)
        {
            // Called after changing the Name
        }
    }
}