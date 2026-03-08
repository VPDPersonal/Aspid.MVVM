using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// MonoBehaviour binder that reads the current state of a <see cref="SphereCollider"/> component
    /// back to the ViewModel in one-way-to-source mode when binding is established.
    /// </summary>
    [AddBinderContextMenu(typeof(SphereCollider))]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Sphere/SphereCollider To Source Binder")]
    public sealed class SphereColliderToSourceMonoBinder : ComponentToSourceMonoBinder<SphereCollider> { }
}