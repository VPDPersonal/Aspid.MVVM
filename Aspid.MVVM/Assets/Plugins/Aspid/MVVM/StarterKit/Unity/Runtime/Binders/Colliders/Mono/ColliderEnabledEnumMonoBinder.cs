using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Collider Binder - Enabled Enum")]
    [AddComponentContextMenu(typeof(Collider),"Add Binder/Collider Binder - Enabled Enum")]
    public sealed class ColliderEnabledEnumMonoBinder : EnumMonoBinder<Collider, bool>
    {
        protected override void SetValue(bool value) =>
            CachedComponent.enabled = value;
    }
}