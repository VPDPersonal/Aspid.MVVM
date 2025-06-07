using UnityEngine;
using Aspid.MVVM.Unity;

namespace Aspid.MVVM.StarterKit.Unity
{
    [AddPropertyContextMenu(typeof(MeshCollider), "m_Convex")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Mesh/MeshCollider Binder - Convex Enum")]
    [AddComponentContextMenu(typeof(MeshCollider),"Add MeshCollider Binder/MeshCollider Binder - Convex Enum")]
    public sealed class MeshColliderConvexEnumMonoBinder : EnumComponentMonoBinder<MeshCollider, bool>
    {
        protected override void SetValue(bool value) =>
            CachedComponent.convex = value;
    }
}