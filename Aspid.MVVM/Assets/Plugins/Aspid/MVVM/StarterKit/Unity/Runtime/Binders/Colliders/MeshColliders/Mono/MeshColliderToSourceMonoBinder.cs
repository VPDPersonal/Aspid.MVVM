using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// MonoBehaviour binder that reads the current state of a <see cref="MeshCollider"/> component
    /// back to the ViewModel in one-way-to-source mode when binding is established.
    /// </summary>
    [AddBinderContextMenu(typeof(MeshCollider))]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Mesh/MeshCollider To Source Binder")]
    public sealed class MeshColliderToSourceMonoBinder : ComponentToSourceMonoBinder<MeshCollider> { }
}