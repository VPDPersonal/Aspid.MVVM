using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentToSourceMonoBinder{TComponent}"/> for <see cref="Transform"/>.
    /// </summary>
    [AddBinderContextMenu(typeof(Transform))]
    [AddComponentMenu("Aspid/MVVM/Binders/Transform/Transform To Source Binder")]
    public sealed class TransformToSourceMonoBinder : ComponentToSourceMonoBinder<Transform> { }
}
