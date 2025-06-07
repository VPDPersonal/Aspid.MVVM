using UnityEngine;
using Aspid.MVVM.Unity;

namespace Aspid.MVVM.StarterKit.Unity
{
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Collider Binder - Enabled Enum")]
    [AddComponentContextMenu(typeof(Collider),"Add Binder/Collider Binder - Enabled Enum")]
    public sealed class ColliderEnabledEnumMonoBinder : EnumComponentMonoBinder<Collider, bool>
    {
        protected override void SetValue(bool value) =>
            CachedComponent.enabled = value;
    }
}