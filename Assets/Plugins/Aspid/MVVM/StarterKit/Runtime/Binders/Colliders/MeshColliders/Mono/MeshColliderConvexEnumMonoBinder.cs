using UnityEngine;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Mesh/MeshCollider Binder - Convex Enum")]
    public sealed class MeshColliderConvexEnumMonoBinder : EnumComponentMonoBinder<MeshCollider, bool>
    {
        protected override void SetValue(bool value) =>
            CachedComponent.convex = value;
    }
}