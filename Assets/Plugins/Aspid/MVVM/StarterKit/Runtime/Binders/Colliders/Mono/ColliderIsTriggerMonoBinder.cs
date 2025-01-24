using UnityEngine;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Collider Binder - IsTrigger")]
    public class ColliderIsTriggerMonoBinder : ComponentMonoBinder<Collider>, IBinder<bool>
    {
        [Header("Converter")]
        [SerializeField] private bool _isInvert;
        
        public void SetValue(bool value) =>
            CachedComponent.isTrigger = _isInvert ? !value : value;
    }
}