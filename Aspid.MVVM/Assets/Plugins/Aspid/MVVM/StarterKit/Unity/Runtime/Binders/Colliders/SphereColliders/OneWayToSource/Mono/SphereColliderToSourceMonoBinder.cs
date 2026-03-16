using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentToSourceMonoBinder{SphereCollider}"/> that sends the cached <see cref="SphereCollider"/>
    /// component reference to the ViewModel when binding is established.
    /// </summary>
    [AddBinderContextMenu(typeof(SphereCollider))]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Sphere/SphereCollider To Source Binder")]
    public sealed class SphereColliderToSourceMonoBinder : ComponentToSourceMonoBinder<SphereCollider> { }
}