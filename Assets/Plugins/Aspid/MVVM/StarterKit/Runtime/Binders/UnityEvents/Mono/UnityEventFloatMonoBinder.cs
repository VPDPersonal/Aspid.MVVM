using UnityEngine;
using Aspid.MVVM.Mono;
using UnityEngine.Events;
using Aspid.MVVM.Mono.Generation;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Binders/UnityEvent/UnityEvent Binder - Float")]
    public sealed partial class UnityEventFloatMonoBinder : MonoBinder, IBinder<float>
    {
        public event UnityAction<float> Set
        {
            add => _set.AddListener(value);
            remove => _set.RemoveListener(value);
        }
        
        [Header("Converters")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#if UNITY_2023_1_OR_NEWER
        private IConverter<float, float> _converter;
#else
        private IConverterFloatToFloat _converter;
#endif
        
        [Header("Events")]
        [SerializeField] private UnityEvent<float> _set;

        [BinderLog]
        public void SetValue(float value)
        {
            value = _converter?.Convert(value) ?? value;
            _set?.Invoke(value);
        }
    }
}