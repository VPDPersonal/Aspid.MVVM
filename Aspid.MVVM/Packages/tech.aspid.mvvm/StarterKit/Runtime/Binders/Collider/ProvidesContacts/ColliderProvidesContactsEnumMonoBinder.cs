using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{TComponent, TValue}"/> that sets <see cref="Collider.providesContacts"/>.
    /// </summary>
    [AddBinderContextMenu(typeof(Collider), serializePropertyNames: "m_ProvidesContacts", SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Collider Binder – ProvidesContacts Enum")]
    public sealed class ColliderProvidesContactsEnumMonoBinder : EnumMonoBinder<Collider, bool>
    {
        /// <inheritdoc/>
        protected override void SetValue(bool value) =>
            CachedComponent.providesContacts = value;
    }
}
