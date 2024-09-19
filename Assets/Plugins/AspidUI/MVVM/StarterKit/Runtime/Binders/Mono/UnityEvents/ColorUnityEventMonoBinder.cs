using UnityEngine;
using UnityEngine.Events;
using AspidUI.MVVM.Unity.Generation;
using AspidUI.MVVM.StarterKit.Converters;
using MonoBinder = AspidUI.MVVM.Unity.MonoBinder;

namespace AspidUI.MVVM.StarterKit.Binders.Mono.UnityEvents
{
    [AddComponentMenu("UI/Binders/UnityEvent/UnityEvent Binder - Color")]
    public sealed partial class ColorUnityEventMonoBinder : MonoBinder, IColorBinder
    {
        public event UnityAction<Color> ColorValueSet
        {
            add => _colorValueSet.AddListener(value);
            remove => _colorValueSet.RemoveListener(value);
        }
        
        [Header("Converter")]
#if ASPID_UI_SERIALIZE_REFERENCE_DROPDOWN_INTEGRATION
        [SerializeReferenceDropdown]
#endif
        [SerializeReference] private IConverterColorToColor _colorConverter;
        
        [Header("Events")]
        [SerializeField] private UnityEvent<Color> _colorValueSet;
        
        [BinderLog]
        public void SetValue(Color value)
        {
            value = _colorConverter?.Convert(value) ?? value;
            _colorValueSet?.Invoke(value);
        }
    }
}