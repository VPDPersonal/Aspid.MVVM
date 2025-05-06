using UnityEngine;

namespace Aspid.MVVM.Unity
{
    [ViewModel]
    public abstract partial class ScriptableViewModel : ScriptableObject
    {
        protected virtual void OnValidate() =>
            this.InvokeAllChangedEventsDebug();
    }
}