using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Mesh/MeshCollider Binder – Convex EnumGroup")]
    [AddBinderContextMenu(typeof(MeshCollider), serializePropertyNames: "m_Convex", SubPath = "EnumGroup")]
    public sealed class MeshColliderConvexEnumGroupMonoBinder : EnumGroupMonoBinder<MeshCollider, bool>
    {
        protected override void SetValue(MeshCollider element, bool value) =>
            element.convex = value;
    }
}