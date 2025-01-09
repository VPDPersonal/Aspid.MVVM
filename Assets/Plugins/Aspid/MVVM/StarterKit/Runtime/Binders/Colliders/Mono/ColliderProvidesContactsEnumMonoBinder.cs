using UnityEngine;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("MVVM/Binders/Collider/Collider Binder - ProvidesContacts Enum")]
    public sealed class ColliderProvidesContactsEnumMonoBinder : EnumComponentMonoBinder<Collider, bool>
    {
        protected override void SetValue(bool value) =>
            CachedComponent.providesContacts = value;
    }
}