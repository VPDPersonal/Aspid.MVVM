using UnityEngine;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Binders/Collider/Collider Binder - Enabled")]
    public class ColliderEnabledMonoBinder : ComponentMonoBinder<Collider>, IBinder<bool>
    {
        [Header("Converter")]
        [SerializeField] private bool _isInvert;
        
        public void SetValue(bool value) =>
            CachedComponent.enabled = _isInvert ? !value : value;
    }
}