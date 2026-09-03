using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentToSourceMonoBinder{TComponent}"/> for <see cref="MeshCollider"/>.
    /// </summary>
    [AddBinderContextMenu(typeof(MeshCollider))]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Mesh/MeshCollider To Source Binder")]
    public sealed class MeshColliderToSourceMonoBinder : ComponentToSourceMonoBinder<MeshCollider> { }
}
