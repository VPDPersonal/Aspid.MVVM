using UnityEngine;
using UnityEngine.Events;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Events
{
    public class ColorEventBinder : MonoBinder, IColorBinder
    {
        public event UnityAction<Color> ColorValueSet
        {
            add => _colorValueSet.AddListener(value);
            remove => _colorValueSet.RemoveListener(value);
        }
        
        [Header("Events")]
        [SerializeField] private UnityEvent<Color> _colorValueSet;
        
        public void SetValue(Color value) =>
            _colorValueSet?.Invoke(value);
    }
}