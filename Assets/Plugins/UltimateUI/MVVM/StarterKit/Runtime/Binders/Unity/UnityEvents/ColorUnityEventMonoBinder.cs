using UnityEngine;
using UnityEngine.Events;
using UltimateUI.MVVM.Unity;
using UltimateUI.MVVM.Unity.Generation;
using UltimateUI.MVVM.StarterKit.Converters;

namespace UltimateUI.MVVM.StarterKit.Binders.Unity.UnityEvents
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