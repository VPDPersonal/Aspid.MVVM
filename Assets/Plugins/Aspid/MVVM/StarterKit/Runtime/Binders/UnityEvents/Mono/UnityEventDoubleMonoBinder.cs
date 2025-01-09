using UnityEngine;
using Aspid.MVVM.Mono;
using UnityEngine.Events;
using Aspid.MVVM.Mono.Generation;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("MVVM/Binders/UnityEvent/UnityEvent Binder - Double")]
    public sealed partial class UnityEventDoubleMonoBinder : MonoBinder, IBinder<double>
    {
        public event UnityAction<double> Set
        {
            add => _set.AddListener(value);
            remove => _set.RemoveListener(value);
        }
        
        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#if UNITY_2023_1_OR_NEWER
        private IConverter<double, double> _converter;
#else
        private IConverterDouble _converter;
#endif
        
        [Header("Events")]
        [SerializeField] private UnityEvent<double> _set;

        [BinderLog]
        public void SetValue(double value)
        {
            value = _converter?.Convert(value) ?? value;
            _set?.Invoke(value);
        }
    }
}