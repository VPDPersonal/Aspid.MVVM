using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Collider Binder – ProvidesContacts")]
    [AddBinderContextMenu(typeof(Collider), serializePropertyNames: "m_ProvidesContacts")]
    public partial class ColliderProvidesContactsMonoBinder : ComponentMonoBinder<Collider>, IBinder<bool>
    {
        [SerializeField] private bool _isInvert;
        
        [BinderLog]
        public void SetValue(bool value) =>
            CachedComponent.providesContacts = _isInvert ? !value : value;
    }
}