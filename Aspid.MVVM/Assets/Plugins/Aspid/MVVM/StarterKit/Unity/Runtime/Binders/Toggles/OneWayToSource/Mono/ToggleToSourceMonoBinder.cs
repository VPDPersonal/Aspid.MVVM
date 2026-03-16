using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentToSourceMonoBinder{Toggle}"/> that sends the current bound property value
    /// of a <see cref="Toggle"/> back to the ViewModel when binding is established.
    /// </summary>
    [AddBinderContextMenu(typeof(Toggle))]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Toggle/Toggle To Source Binder")]
    public sealed class ToggleToSourceMonoBinder : ComponentToSourceMonoBinder<Toggle> { }
}