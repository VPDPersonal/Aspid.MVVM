using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(RectTransform))]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/RectTransform/RectTransform To Source Binder")]
    public sealed class RectTransformToSourceMonoBinder : ComponentToSourceMonoBinder<RectTransform> { }
}