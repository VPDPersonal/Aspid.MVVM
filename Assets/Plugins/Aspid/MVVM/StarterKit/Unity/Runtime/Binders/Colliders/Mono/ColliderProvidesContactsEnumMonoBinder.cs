using UnityEngine;
using Aspid.MVVM.Unity;

namespace Aspid.MVVM.StarterKit.Unity
{
    [AddPropertyContextMenu(typeof(Collider), "m_ProvidesContacts")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Collider Binder - ProvidesContacts Enum")]
    [AddComponentContextMenu(typeof(Collider),"Add Binder/Collider Binder - ProvidesContacts Enum")]
    public sealed class ColliderProvidesContactsEnumMonoBinder : EnumMonoBinder<Collider, bool>
    {
        protected override void SetValue(bool value) =>
            CachedComponent.providesContacts = value;
    }
}