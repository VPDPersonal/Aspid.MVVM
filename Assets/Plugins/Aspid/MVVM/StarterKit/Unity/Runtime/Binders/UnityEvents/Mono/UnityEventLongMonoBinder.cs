using UnityEngine;
using Aspid.MVVM.Unity;
using UnityEngine.Events;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<long, long>;
#else
using Converter = Aspid.MVVM.StarterKit.Unity.IConverterLong;
#endif

namespace Aspid.MVVM.StarterKit.Unity
{
    [AddComponentMenu("Aspid/MVVM/Binders/UnityEvent/UnityEvent Binder - Long")]
    public sealed partial class UnityEventLongMonoBinder : MonoBinder, INumberBinder
    {
        public event UnityAction<long> Set
        {
            add => _set.AddListener(value);
            remove => _set.RemoveListener(value);
        }
        
        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;
        
        [Header("Events")]
        [SerializeField] private UnityEvent<long> _set;
        
        [BinderLog]
        public void SetValue(int value) =>
            SetValue((long)value);
        
        [BinderLog]
        public void SetValue(long value)
        {
            value = _converter?.Convert(value) ?? value;
            _set?.Invoke(value);
        }

        [BinderLog]
        public void SetValue(float value) =>
            SetValue((long)value);

        [BinderLog]
        public void SetValue(double value) =>
            SetValue((long)value);
    }
}