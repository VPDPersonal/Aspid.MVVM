using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentToSourceMonoBinder{CapsuleCollider}"/> that sends the cached <see cref="CapsuleCollider"/>
    /// component reference to the ViewModel when binding is established.
    /// </summary>
    [AddBinderContextMenu(typeof(CapsuleCollider))]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Capsule/CapsuleCollider To Source Binder")]
    public sealed class CapsuleColliderToSourceMonoBinder : ComponentToSourceMonoBinder<CapsuleCollider> { }
}