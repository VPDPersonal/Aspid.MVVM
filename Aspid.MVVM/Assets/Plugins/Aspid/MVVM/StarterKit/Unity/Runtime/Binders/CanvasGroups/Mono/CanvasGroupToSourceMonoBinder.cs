using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentToSourceMonoBinder{CanvasGroup}"/> that sends the cached <see cref="CanvasGroup"/>
    /// component reference to the ViewModel when binding is established.
    /// </summary>
    [AddBinderContextMenu(typeof(CanvasGroup))]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/CanvasGroup/CanvasGroup To Source Binder")]
    public sealed class CanvasGroupToSourceMonoBinder : ComponentToSourceMonoBinder<CanvasGroup> { }
}