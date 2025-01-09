using UnityEngine;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("MVVM/Binders/Collider/MeshCollider Binder - Convex Enum")]
    public sealed class MeshColliderConvexEnumMonoBinder : EnumComponentMonoBinder<MeshCollider, bool>
    {
        protected override void SetValue(bool value) =>
            CachedComponent.convex = value;
    }
}