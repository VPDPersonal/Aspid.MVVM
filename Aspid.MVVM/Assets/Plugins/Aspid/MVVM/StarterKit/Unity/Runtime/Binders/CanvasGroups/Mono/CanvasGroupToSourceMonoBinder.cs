using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// MonoBehaviour binder that reads the current state of a <see cref="CanvasGroup"/> component
    /// back to the ViewModel in one-way-to-source mode when binding is established.
    /// </summary>
    [AddBinderContextMenu(typeof(CanvasGroup))]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/CanvasGroup/CanvasGroup To Source Binder")]
    public sealed class CanvasGroupToSourceMonoBinder : ComponentToSourceMonoBinder<CanvasGroup> { }
}