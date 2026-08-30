using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent,TProperty}">ComponentMonoBinder&lt;MeshCollider, Mesh&gt;</see> that binds the <see cref="MeshCollider.sharedMesh"/> property.
    /// </summary>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(MeshCollider), serializePropertyNames: "m_Mesh")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Mesh/MeshCollider Binder – Mesh")]
    public class MeshColliderMeshMonoBinder : ComponentMonoBinder<MeshCollider, Mesh>
    {
        /// <inheritdoc/>
        protected sealed override Mesh Property
        {
            get => CachedComponent.sharedMesh;
            set => CachedComponent.sharedMesh = value;
        }
    }
}