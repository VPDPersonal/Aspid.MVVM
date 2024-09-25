using UnityEngine;
using UnityEngine.Events;
using Aspid.UI.MVVM.Mono.Generation;
using Aspid.UI.MVVM.StarterKit.Converters;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono.UnityEvents
{
    [AddComponentMenu("UI/Binders/UnityEvent/UnityEvent Binder - Color")]
    public sealed partial class ColorUnityEventMonoBinder : Aspid.UI.MVVM.Mono.MonoBinder, IColorBinder
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