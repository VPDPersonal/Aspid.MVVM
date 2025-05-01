using UnityEngine;

namespace Aspid.MVVM.StarterKit.Unity
{
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Collider Binder - IsTrigger Enum")]
    public sealed class ColliderIsTriggerEnumMonoBinder : EnumComponentMonoBinder<Collider, bool>
    {
        protected override void SetValue(bool value) =>
            CachedComponent.isTrigger = value;
    }
}