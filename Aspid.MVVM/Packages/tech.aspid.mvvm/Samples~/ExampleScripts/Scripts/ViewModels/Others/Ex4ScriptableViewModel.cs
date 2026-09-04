using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Samples.ExampleScripts.ViewModels.Others
{
    // A ViewModel that is an asset: assign it to ViewInitializer with Resolve Type = ScriptableObject.
    [ViewModel]
    [CreateAssetMenu(menuName = "Aspid/MVVM/Samples/Ex4 Scriptable ViewModel")]
    public partial class Ex4ScriptableViewModel : ScriptableViewModel
    {
        [Bind]
        [SerializeField] private string _text;
    }
}
