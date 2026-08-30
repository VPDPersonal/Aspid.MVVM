using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentToSourceMonoBinder{RectTransform}"/> that sends the cached <see cref="RectTransform"/>
    /// component reference to the ViewModel when binding is established.
    /// </summary>
    [AddBinderContextMenu(typeof(RectTransform))]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/RectTransform/RectTransform To Source Binder")]
    public sealed class RectTransformToSourceMonoBinder : ComponentToSourceMonoBinder<RectTransform> { }
}