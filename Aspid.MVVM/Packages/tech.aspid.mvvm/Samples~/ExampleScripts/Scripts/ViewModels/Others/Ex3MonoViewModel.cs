using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Samples.ExampleScripts.ViewModels.Others
{
    // A ViewModel that is a component: assign it to ViewInitializer with Resolve Type = Component.
    [ViewModel]
    public partial class Ex3MonoViewModel : MonoViewModel
    {
        [Bind]
        [SerializeField] private string _text;
    }
}
