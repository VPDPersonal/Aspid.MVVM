using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentToSourceMonoBinder{TComponent}"/> for <see cref="SphereCollider"/>.
    /// </summary>
    [AddBinderContextMenu(typeof(SphereCollider))]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Sphere/SphereCollider To Source Binder")]
    public sealed class SphereColliderToSourceMonoBinder : ComponentToSourceMonoBinder<SphereCollider> { }
}
