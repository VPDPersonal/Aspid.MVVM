using UnityEngine;
using UnityEngine.Events;
using UltimateUI.MVVM.Unity;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Bindings.Events
{
    public partial class ColorEventBinding : MonoBinding, ITargetBinding, IColorTargetBinding
    {
        public event UnityAction<Color> ColorValueSet
        {
            add => _colorValueSet.AddListener(value);
            remove => _colorValueSet.RemoveListener(value);
        }
        
        [Header("Events")]
        [SerializeField] private UnityEvent<Color> _colorValueSet;
        
#if !ULTIMATE_UI_MVVM_STARTER_KIT_BINDER_LOG_GENERATOR_DISABLED
        [BindingLog]
#endif
        public void SetValue(Color value) =>
            _colorValueSet?.Invoke(value);
    }
}