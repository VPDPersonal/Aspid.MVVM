using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(CapsuleCollider))]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Capsule/CapsuleCollider To Source Binder")]
    public sealed class CapsuleColliderToSourceMonoBinder : ComponentToSourceMonoBinder<CapsuleCollider> { }
}