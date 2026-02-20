using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(MeshCollider))]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Mesh/MeshCollider To Source Binder")]
    public sealed class MeshColliderToSourceMonoBinder : ComponentToSourceMonoBinder<MeshCollider> { }
}