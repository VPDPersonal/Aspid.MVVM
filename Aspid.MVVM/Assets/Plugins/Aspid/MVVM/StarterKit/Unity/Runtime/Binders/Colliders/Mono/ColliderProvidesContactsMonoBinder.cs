using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// MonoBehaviour binder that sets the <see cref="Collider.providesContacts"/> property on a <see cref="Collider"/>
    /// when the bound ViewModel value changes.
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established the current value
    /// is sent back to the ViewModel. Supports optional value inversion.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Collider Binder – ProvidesContacts")]
    [AddBinderContextMenu(typeof(Collider), serializePropertyNames: "m_ProvidesContacts")]
    public class ColliderProvidesContactsMonoBinder : ComponentBoolMonoBinder<Collider>
    {
        protected sealed override bool Property
        {
            get => CachedComponent.providesContacts;
            set => CachedComponent.providesContacts = value;
        }
    }
}