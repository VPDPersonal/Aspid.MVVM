using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentToSourceMonoBinder{MeshCollider}"/> that sends the cached <see cref="MeshCollider"/>
    /// component reference to the ViewModel when binding is established.
    /// </summary>
    [AddBinderContextMenu(typeof(MeshCollider))]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Mesh/MeshCollider To Source Binder")]
    public sealed class MeshColliderToSourceMonoBinder : ComponentToSourceMonoBinder<MeshCollider> { }
}