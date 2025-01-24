using UnityEngine;
using Aspid.MVVM.Mono;
using UnityEngine.Events;
using Aspid.MVVM.Mono.Generation;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.Converters.IConverter<float, bool>;
#else
using Converter = Aspid.MVVM.StarterKit.Converters.IConverterFloatToBool;
#endif

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Aspid/MVVM/Binders/UnityEvent/UnityEvent Binder - Number Condition")]
    public sealed partial class UnityEventNumberConditionMonoBinder : MonoBinder, INumberBinder
    {
        public event UnityAction<bool> Set
        {
            add => _set.AddListener(value);
            remove => _set.RemoveListener(value);
        }
        
        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;
        
        [Header("Events")]
        [SerializeField] private UnityEvent<bool> _set;
        
        [BinderLog]
        public void SetValue(int value) =>
            SetValue((float)value);

        [BinderLog]
        public void SetValue(long value) =>
            SetValue((float)value);

        [BinderLog]
        public void SetValue(float value) =>
            _set.Invoke(_converter.Convert(value));

        [BinderLog]
        public void SetValue(double value) =>
            SetValue((float)value);
    }
}