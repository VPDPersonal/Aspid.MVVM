using UnityEngine;

namespace Aspid.MVVM.Unity
{
    /// <summary>
    /// Represents a base class for ViewModels in a Unity context derived from <see cref="ScriptableObject"/>.
    /// </summary>
    [ViewModel]
    public abstract partial class ScriptableViewModel : ScriptableObject
    {
        protected virtual void OnValidate() =>
            NotifyAll();
    }
}