using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// MonoBehaviour binder that sets the <see cref="Collider.providesContacts"/> property on a group of <see cref="Collider"/>
    /// components, applying the configured selected or default value to each entry based on the bound
    /// enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Collider Binder – ProvidesContacts EnumGroup")]
    [AddBinderContextMenu(typeof(Collider), serializePropertyNames: "m_ProvidesContacts", SubPath = "EnumGroup")]
    public sealed class ColliderProvidesContactsEnumGroupMonoBinder : EnumGroupMonoBinder<Collider, bool>
    {
        protected override void SetValue(Collider element, bool value) =>
            element.providesContacts = value;
    }
}