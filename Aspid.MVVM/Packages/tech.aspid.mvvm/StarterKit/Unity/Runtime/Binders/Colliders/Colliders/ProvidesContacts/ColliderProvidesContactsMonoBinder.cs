using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent,TProperty}"/> that binds the <see cref="Collider.providesContacts"/> property.
    /// </summary>
    [GenerateSerializableBinder]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Collider Binder – ProvidesContacts")]
    [AddBinderContextMenu(typeof(Collider), serializePropertyNames: "m_ProvidesContacts")]
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