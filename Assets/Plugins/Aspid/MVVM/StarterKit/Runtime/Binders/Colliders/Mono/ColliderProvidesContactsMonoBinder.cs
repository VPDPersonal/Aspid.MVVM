using UnityEngine;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Collider Binder - ProvidesContacts")]
    public class ColliderProvidesContactsMonoBinder : ComponentMonoBinder<Collider>, IBinder<bool>
    {
        [Header("Converter")]
        [SerializeField] private bool _isInvert;
        
        public void SetValue(bool value) =>
            CachedComponent.providesContacts = _isInvert ? !value : value;
    }
}