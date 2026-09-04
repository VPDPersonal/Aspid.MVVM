using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentToSourceMonoBinder{TComponent}"/> for <see cref="CanvasGroup"/>.
    /// </summary>
    [AddBinderContextMenu(typeof(CanvasGroup))]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/CanvasGroup/CanvasGroup To Source Binder")]
    public sealed class CanvasGroupToSourceMonoBinder : ComponentToSourceMonoBinder<CanvasGroup> { }
}
