using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Mesh/MeshCollider Binder – Convex Enum")]
    [AddBinderContextMenu(typeof(MeshCollider), serializePropertyNames: "m_Convex", SubPath = "Enum")]
    public sealed class MeshColliderConvexEnumMonoBinder : EnumMonoBinder<MeshCollider, bool>
    {
        protected override void SetValue(bool value) =>
            CachedComponent.convex = value;
    }
}