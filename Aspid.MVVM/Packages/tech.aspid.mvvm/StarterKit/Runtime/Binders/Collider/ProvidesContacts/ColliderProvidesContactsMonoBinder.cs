using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent, TProperty}"/> that binds <see cref="Collider.providesContacts"/>.
    /// </summary>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(Collider), serializePropertyNames: "m_ProvidesContacts")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Collider Binder – ProvidesContacts")]
    public class ColliderProvidesContactsMonoBinder : ComponentMonoBinder<Collider, bool>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => CachedComponent.providesContacts;
            set => CachedComponent.providesContacts = value;
        }
    }
}
