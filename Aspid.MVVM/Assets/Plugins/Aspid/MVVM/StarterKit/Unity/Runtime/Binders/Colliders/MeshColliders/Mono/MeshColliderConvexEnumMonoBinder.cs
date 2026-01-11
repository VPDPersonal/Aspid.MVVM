using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(MeshCollider), serializePropertyNames: "m_Convex")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Mesh/MeshCollider Binder – Convex Enum")]
    public sealed class MeshColliderConvexEnumMonoBinder : EnumMonoBinder<MeshCollider, bool>
    {
        protected override void SetValue(bool value) =>
            CachedComponent.convex = value;
    }
}