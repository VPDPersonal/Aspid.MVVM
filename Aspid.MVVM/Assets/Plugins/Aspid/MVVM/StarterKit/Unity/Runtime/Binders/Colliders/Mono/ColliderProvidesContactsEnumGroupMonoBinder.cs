using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(Collider), serializePropertyNames: "m_ProvidesContacts")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Collider Binder – ProvidesContacts EnumGroup")]
    public sealed class ColliderProvidesContactsEnumGroupMonoBinder : EnumGroupMonoBinder<Collider, bool>
    {
        protected override void SetValue(Collider element, bool value) =>
            element.providesContacts = value;
    }
}