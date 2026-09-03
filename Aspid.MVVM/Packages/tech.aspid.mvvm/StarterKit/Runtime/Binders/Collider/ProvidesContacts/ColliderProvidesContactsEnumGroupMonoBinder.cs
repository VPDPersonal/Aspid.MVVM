using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{TElement, TValue}"/> that sets <see cref="Collider.providesContacts"/> on each element.
    /// </summary>
    [AddBinderContextMenu(typeof(Collider), serializePropertyNames: "m_ProvidesContacts", SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Collider Binder – ProvidesContacts EnumGroup")]
    public sealed class ColliderProvidesContactsEnumGroupMonoBinder : EnumGroupMonoBinder<Collider, bool>
    {
        /// <inheritdoc/>
        protected override void SetValue(Collider element, bool value) =>
            element.providesContacts = value;
    }
}
