using UnityEngine;

namespace Aspid.MVVM.ExampleScripts.NewViewModels
{
    [ViewModel]
    public partial class InheritedScriptableObjectViewModel : ScriptableObject
    {
        private void OnValidate() =>
            this.InvokeAllChangedEventsDebug();
    }
}