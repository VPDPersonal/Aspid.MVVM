using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{T1, T2}">EnumGroupMonoBinder&lt;Collider, bool&gt;</see> that sets the <see cref="Collider.providesContacts"/>
    /// property on each element based on the bound enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Collider Binder – ProvidesContacts EnumGroup")]
    [AddBinderContextMenu(typeof(Collider), serializePropertyNames: "m_ProvidesContacts", SubPath = "EnumGroup")]
    public sealed class ColliderProvidesContactsEnumGroupMonoBinder : EnumGroupMonoBinder<Collider, bool>
    {
        /// <inheritdoc/>
        protected override void SetValue(Collider element, bool value) =>
            element.providesContacts = value;
    }
}