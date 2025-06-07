using UnityEngine;
using Aspid.MVVM.Unity;

namespace Aspid.MVVM.StarterKit.Unity
{
    [AddPropertyContextMenu(typeof(Collider), "m_ProvidesContacts")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Collider Binder - ProvidesContacts EnumGroup")]
    [AddComponentContextMenu(typeof(Collider),"Add Binder/Collider Binder - ProvidesContacts EnumGroup")]
    public sealed class ColliderProvidesContactsEnumGroupMonoBinder : EnumGroupMonoBinder<Collider>
    {
        [Header("Parameters")]
        [SerializeField] private bool _defaultValue;
        [SerializeField] private bool _selectedValue;
        
        protected override void SetDefaultValue(Collider element) =>
            element.providesContacts = _defaultValue;

        protected override void SetSelectedValue(Collider element) =>
            element.providesContacts = _selectedValue;
    }
}