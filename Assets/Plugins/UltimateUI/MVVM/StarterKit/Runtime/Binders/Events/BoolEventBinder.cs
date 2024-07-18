using UnityEngine;
using UnityEngine.Events;
using UltimateUI.MVVM.Unity;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.Events
{
    public partial class BoolEventBinder : MonoBinder, IBinder<bool>
    {
        public event UnityAction<bool> BoolValueSet
        {
            add => _boolValueSet.AddListener(value);
            remove => _boolValueSet.RemoveListener(value);
        }
        
        [Header("Events")]
        [SerializeField] private UnityEvent<bool> _boolValueSet;
        
#if !ULTIMATE_UI_MVVM_STARTER_KIT_BINDER_LOG_GENERATOR_DISABLED
        [BinderLog]
#endif
        public void SetValue(bool value) =>
            _boolValueSet?.Invoke(value);
    }
}