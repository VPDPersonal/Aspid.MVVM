using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{TElement, TValue}"/> that sets <see cref="MeshCollider.sharedMesh"/> on each element.
    /// </summary>
    [AddBinderContextMenu(typeof(MeshCollider), serializePropertyNames: "m_Mesh", SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Mesh/MeshCollider Binder – Mesh EnumGroup")]
    public sealed class MeshColliderMeshEnumGroupMonoBinder : EnumGroupMonoBinder<MeshCollider, Mesh>
    {
        /// <inheritdoc/>
        protected override void SetValue(MeshCollider element, Mesh value) =>
            element.sharedMesh = value;
    }
}
