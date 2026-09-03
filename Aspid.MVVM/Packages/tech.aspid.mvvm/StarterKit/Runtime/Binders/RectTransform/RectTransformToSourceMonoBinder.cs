using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentToSourceMonoBinder{TComponent}"/> for <see cref="RectTransform"/>.
    /// </summary>
    [AddBinderContextMenu(typeof(RectTransform))]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/RectTransform/RectTransform To Source Binder")]
    public sealed class RectTransformToSourceMonoBinder : ComponentToSourceMonoBinder<RectTransform> { }
}
