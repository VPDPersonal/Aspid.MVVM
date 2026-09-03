using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{TElement, TValue}"/> that sets <see cref="MeshCollider.convex"/> on each element.
    /// </summary>
    [AddBinderContextMenu(typeof(MeshCollider), serializePropertyNames: "m_Convex", SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Mesh/MeshCollider Binder – Convex EnumGroup")]
    public sealed class MeshColliderConvexEnumGroupMonoBinder : EnumGroupMonoBinder<MeshCollider, bool>
    {
        /// <inheritdoc/>
        protected override void SetValue(MeshCollider element, bool value) =>
            element.convex = value;
    }
}
