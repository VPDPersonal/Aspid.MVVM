using UnityEngine;
using UnityEngine.Events;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<double, double>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterDouble;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddPropertyContextMenu(typeof(double))]
    [AddComponentMenu("Aspid/MVVM/Binders/UnityEvent/UnityEvent Binder - Double")]
    [AddComponentContextMenu(typeof(Component),"Add General Binder/UnityEvent/UnityEvent Binder - Double")]
    public sealed partial class UnityEventDoubleMonoBinder : MonoBinder, INumberBinder
    {
        public event UnityAction<double> Set
        {
            add => _set.AddListener(value);
            remove => _set.RemoveListener(value);
        }
        
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;
        
        [Header("Events")]
        [SerializeField] private UnityEvent<double> _set;
        
        [BinderLog]
        public void SetValue(int value) =>
            SetValue((double)value);

        [BinderLog]
        public void SetValue(long value) =>
            SetValue((double)value);

        [BinderLog]
        public void SetValue(float value) =>
            SetValue((double)value);
        
        [BinderLog]
        public void SetValue(double value)
        {
            value = _converter?.Convert(value) ?? value;
            _set?.Invoke(value);
        }
    }
}