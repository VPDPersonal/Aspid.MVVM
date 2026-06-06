using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentToSourceMonoBinder{RectTransform}"/> that sends the current bound property value
    /// of a <see cref="RectTransform"/> back to the ViewModel when binding is established.
    /// </summary>
    [AddBinderContextMenu(typeof(RectTransform))]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/RectTransform/RectTransform To Source Binder")]
    public sealed class RectTransformToSourceMonoBinder : ComponentToSourceMonoBinder<RectTransform> { }
}