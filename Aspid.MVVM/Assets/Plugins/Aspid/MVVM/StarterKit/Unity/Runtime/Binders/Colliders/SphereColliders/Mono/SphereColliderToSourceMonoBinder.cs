using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(SphereCollider))]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Sphere/SphereCollider To Source Binder")]
    public sealed class SphereColliderToSourceMonoBinder : ComponentToSourceMonoBinder<SphereCollider> { }
}