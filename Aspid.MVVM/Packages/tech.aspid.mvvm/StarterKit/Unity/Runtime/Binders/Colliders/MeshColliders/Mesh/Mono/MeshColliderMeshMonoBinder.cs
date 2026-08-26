using UnityEngine;
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Mesh, UnityEngine.Mesh>;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{T1, T2, T3}">ComponentMonoBinder&lt;MeshCollider, Mesh, IConverter&lt;Mesh, Mesh&gt;&gt;</see> that binds the <see cref="MeshCollider.sharedMesh"/> property.
    /// </summary>
    /// <remarks>
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established, the current shared mesh value
    /// is sent back to the ViewModel.
    /// </remarks>
    [AddBinderContextMenu(typeof(MeshCollider), serializePropertyNames: "m_Mesh")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Mesh/MeshCollider Binder – Mesh")]
    public class MeshColliderMeshMonoBinder : ComponentMonoBinder<MeshCollider, Mesh, Converter>
    {
        /// <inheritdoc/>
        protected sealed override Mesh Property
        {
            get => CachedComponent.sharedMesh;
            set => CachedComponent.sharedMesh = value;
        }
    }
}