using UnityEngine;
using UnityEngine.Events;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.Events
{
    public partial class QuaternionEventBinder : MonoBinder, IBinder<Quaternion>
    {
        public event UnityAction<Quaternion> QuaternionValueSet
        {
            add => _quaternionValueSet.AddListener(value);
            remove => _quaternionValueSet.RemoveListener(value);
        }
        
        [Header("Events")]
        [SerializeField] private UnityEvent<Quaternion> _quaternionValueSet;
        
                
#if !ULTIMATE_UI_MVVM_STARTER_KIT_BINDER_LOG_GENERATOR_DISABLED
        [BinderLog]
#endif
        public void SetValue(Quaternion value) =>
            _quaternionValueSet?.Invoke(value);
    }
}