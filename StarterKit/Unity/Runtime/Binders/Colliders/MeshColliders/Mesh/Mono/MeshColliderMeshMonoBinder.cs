using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Mesh, UnityEngine.Mesh>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterMesh;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{MeshCollider, Mesh, IConverter{Mesh, Mesh}}"/> that binds the <see cref="MeshCollider.sharedMesh"/> property.
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