using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Enum binder that sets the <see cref="Collider.providesContacts"/> property on a cached
    /// <see cref="Collider"/> component to a value resolved from an enum bound on the ViewModel.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Collider Binder – ProvidesContacts Enum")]
    [AddBinderContextMenu(typeof(Collider), serializePropertyNames: "m_ProvidesContacts", SubPath = "Enum")]
    public sealed class ColliderProvidesContactsEnumMonoBinder : EnumMonoBinder<Collider, bool>
    {
        protected override void SetValue(bool value) =>
            CachedComponent.providesContacts = value;
    }
}