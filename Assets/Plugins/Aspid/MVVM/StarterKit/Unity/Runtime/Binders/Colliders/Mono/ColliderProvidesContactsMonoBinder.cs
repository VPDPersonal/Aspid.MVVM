using UnityEngine;
using Aspid.MVVM.Unity;

namespace Aspid.MVVM.StarterKit.Unity
{
    [AddPropertyContextMenu(typeof(Collider), "m_ProvidesContacts")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Collider Binder - ProvidesContacts")]
    [AddComponentContextMenu(typeof(Collider),"Add Binder/Collider Binder - ProvidesContacts")]
    public partial class ColliderProvidesContactsMonoBinder : ComponentMonoBinder<Collider>, IBinder<bool>
    {
        [Header("Converter")]
        [SerializeField] private bool _isInvert;
        
        [BinderLog]
        public void SetValue(bool value) =>
            CachedComponent.providesContacts = _isInvert ? !value : value;
    }
}