using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// MonoBehaviour binder that reads the current state of a <see cref="Toggle"/> component
    /// back to the ViewModel in one-way-to-source mode when binding is established.
    /// </summary>
    [AddBinderContextMenu(typeof(Toggle))]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Toggle/Toggle To Source Binder")]
    public sealed class ToggleToSourceMonoBinder : ComponentToSourceMonoBinder<Toggle> { }
}