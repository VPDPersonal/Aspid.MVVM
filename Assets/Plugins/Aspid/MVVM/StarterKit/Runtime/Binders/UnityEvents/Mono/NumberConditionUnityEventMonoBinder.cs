using UnityEngine;
using Aspid.MVVM.Mono;
using UnityEngine.Events;
using Aspid.MVVM.Mono.Generation;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("UI/Binders/UnityEvent/UnityEvent Binder - Number Condition")]
    public partial class NumberConditionUnityEventMonoBinder : MonoBinder, INumberBinder
    {
#if ASPID_MVVM_SERIALIZE_REFERENCE_DROPDOWN_INTEGRATION
        [SerializeReferenceDropdown]
#endif
#if UNITY_2023_1_OR_NEWER
        [SerializeReference] private IConverter<float, bool> _converter;
#else
        [SerializeReference] private IConverterFloatToBool _converter;
#endif
        
        [Header("Events")]
        [SerializeField] private UnityEvent _trueEvent;
        [SerializeField] private UnityEvent _falseEvent;
        
        [BinderLog]
        public void SetValue(int value) =>
            SetValue((float)value);

        [BinderLog]
        public void SetValue(long value) =>
            SetValue((float)value);

        [BinderLog]
        public void SetValue(float value)
        {
            if (_converter.Convert(value)) _trueEvent?.Invoke();
            else _falseEvent?.Invoke();
        }

        [BinderLog]
        public void SetValue(double value) =>
            SetValue((float)value);
    }
}