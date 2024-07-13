using UnityEngine;
using UnityEngine.Events;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.Events
{
    public partial class ColorEventBinder : MonoBinder, IColorBinder
    {
        public event UnityAction<Color> ColorValueSet
        {
            add => _colorValueSet.AddListener(value);
            remove => _colorValueSet.RemoveListener(value);
        }
        
        [Header("Events")]
        [SerializeField] private UnityEvent<Color> _colorValueSet;
        
#if !ULTIMATE_UI_MVVM_STARTER_KIT_BINDER_LOG_GENERATOR_DISABLED
        [BinderLog]
#endif
        public void SetValue(Color value) =>
            _colorValueSet?.Invoke(value);
    }
}