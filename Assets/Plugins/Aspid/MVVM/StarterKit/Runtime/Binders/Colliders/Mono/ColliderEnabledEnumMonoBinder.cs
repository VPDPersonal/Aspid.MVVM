using UnityEngine;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("MVVM/Binders/Collider/Collider Binder - Enabled Enum")]
    public sealed class ColliderEnabledEnumMonoBinder : EnumComponentMonoBinder<Collider, bool>
    {
        protected override void SetValue(bool value) =>
            CachedComponent.enabled = value;
    }
}