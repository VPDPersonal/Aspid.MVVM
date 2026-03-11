using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{MeshCollider, bool}"/> that sets the <see cref="MeshCollider.convex"/>
    /// property on each element based on the bound enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Mesh/MeshCollider Binder – Convex EnumGroup")]
    [AddBinderContextMenu(typeof(MeshCollider), serializePropertyNames: "m_Convex", SubPath = "EnumGroup")]
    public sealed class MeshColliderConvexEnumGroupMonoBinder : EnumGroupMonoBinder<MeshCollider, bool>
    {
        /// <inheritdoc/>
        protected override void SetValue(MeshCollider element, bool value) =>
            element.convex = value;
    }
}