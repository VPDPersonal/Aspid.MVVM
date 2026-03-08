using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// MonoBehaviour binder that reads the current state of a <see cref="CapsuleCollider"/> component
    /// back to the ViewModel in one-way-to-source mode when binding is established.
    /// </summary>
    [AddBinderContextMenu(typeof(CapsuleCollider))]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Capsule/CapsuleCollider To Source Binder")]
    public sealed class CapsuleColliderToSourceMonoBinder : ComponentToSourceMonoBinder<CapsuleCollider> { }
}