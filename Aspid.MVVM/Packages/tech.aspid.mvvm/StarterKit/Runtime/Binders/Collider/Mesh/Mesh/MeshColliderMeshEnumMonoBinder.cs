using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{TComponent, TValue}"/> that sets <see cref="MeshCollider.sharedMesh"/>.
    /// </summary>
    [AddBinderContextMenu(typeof(MeshCollider), serializePropertyNames: "m_Mesh", SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Mesh/MeshCollider Binder – Mesh Enum")]
    public sealed class MeshColliderMeshEnumMonoBinder : EnumMonoBinder<MeshCollider, Mesh>
    {
        /// <inheritdoc/>
        protected override void SetValue(Mesh value) =>
            CachedComponent.sharedMesh = value;
    }
}
