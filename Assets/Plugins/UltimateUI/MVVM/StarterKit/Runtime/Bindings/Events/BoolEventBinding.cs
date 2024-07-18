using UnityEngine;
using UnityEngine.Events;
using UltimateUI.MVVM.Unity;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Bindings.Events
{
    public partial class BoolEventBinding : MonoBinding, ITargetBinding<bool>
    {
        public event UnityAction<bool> BoolValueSet
        {
            add => _boolValueSet.AddListener(value);
            remove => _boolValueSet.RemoveListener(value);
        }
        
        [Header("Events")]
        [SerializeField] private UnityEvent<bool> _boolValueSet;
        
#if !ULTIMATE_UI_MVVM_STARTER_KIT_BINDER_LOG_GENERATOR_DISABLED
        [BindingLog]
#endif
        public void SetValue(bool value) =>
            _boolValueSet?.Invoke(value);
    }
}