using UnityEngine;

namespace Aspid.MVVM.ExampleScripts.NewViewModels
{
    [ViewModel]
    public partial class InheritedMonoBehaviourViewModel : MonoBehaviour
    {
        private void OnValidate() =>
            this.InvokeAllChangedEventsDebug();
    }
}