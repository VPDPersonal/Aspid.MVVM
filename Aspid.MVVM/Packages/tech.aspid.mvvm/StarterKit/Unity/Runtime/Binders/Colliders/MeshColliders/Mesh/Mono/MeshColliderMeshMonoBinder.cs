using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinderWithConverter{T1, T2}">ComponentMonoBinderWithConverter&lt;MeshCollider, Mesh&gt;</see> that binds the <see cref="MeshCollider.sharedMesh"/> property.
    /// </summary>
    [AddBinderContextMenu(typeof(MeshCollider), serializePropertyNames: "m_Mesh")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Mesh/MeshCollider Binder – Mesh")]
    public class MeshColliderMeshMonoBinder : ComponentMonoBinderWithConverter<MeshCollider, Mesh>
    {
        /// <inheritdoc/>
        protected sealed override Mesh Property
        {
            get => CachedComponent.sharedMesh;
            set => CachedComponent.sharedMesh = value;
        }
    }
}