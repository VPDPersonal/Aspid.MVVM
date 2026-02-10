using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(Transform))]
    [AddComponentMenu("Aspid/MVVM/Binders/Transform/Transform To Source Binder")]
    public sealed class TransformToSourceMonoBinder : ComponentToSourceMonoBinder<Transform> { }
}