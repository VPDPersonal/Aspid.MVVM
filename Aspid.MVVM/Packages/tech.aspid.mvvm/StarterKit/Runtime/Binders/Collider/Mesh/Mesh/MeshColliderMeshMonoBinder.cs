using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentObjectMonoBinder{TComponent, TObject}"/> that binds <see cref="MeshCollider.sharedMesh"/>.
    /// </summary>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(MeshCollider), serializePropertyNames: "m_Mesh")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Mesh/MeshCollider Binder – Mesh")]
    public class MeshColliderMeshMonoBinder : ComponentObjectMonoBinder<MeshCollider, Mesh>
    {
        /// <inheritdoc/>
        protected sealed override Mesh Property
        {
            get => CachedComponent.sharedMesh;
            set => CachedComponent.sharedMesh = value;
        }
    }
}
