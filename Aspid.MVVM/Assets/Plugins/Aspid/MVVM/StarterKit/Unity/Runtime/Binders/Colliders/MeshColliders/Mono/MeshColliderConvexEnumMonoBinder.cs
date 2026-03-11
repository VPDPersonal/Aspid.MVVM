using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{MeshCollider, bool}"/> that sets the <see cref="MeshCollider.convex"/>
    /// property based on the bound enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Mesh/MeshCollider Binder – Convex Enum")]
    [AddBinderContextMenu(typeof(MeshCollider), serializePropertyNames: "m_Convex", SubPath = "Enum")]
    public sealed class MeshColliderConvexEnumMonoBinder : EnumMonoBinder<MeshCollider, bool>
    {
        /// <inheritdoc/>
        protected override void SetValue(bool value) =>
            CachedComponent.convex = value;
    }
}