using UnityEngine;
using Aspid.UI.MVVM.Mono;
using UnityEngine.Events;
using Aspid.UI.MVVM.Mono.Generation;
using Aspid.UI.MVVM.StarterKit.Converters;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono.UnityEvents
{
    [AddComponentMenu("UI/Binders/UnityEvent/UnityEvent Binder - Number Condition")]
    public partial class NumberConditionUnityEventMonoBinder : MonoBinder, INumberBinder
    {
#if !UNITY_6000_0_OR_NEWER
#if ASPID_UI_SERIALIZE_REFERENCE_DROPDOWN_INTEGRATION
        [SerializeReferenceDropdown]
#endif // ASPID_UI_SERIALIZE_REFERENCE_DROPDOWN_INTEGRATION
        [SerializeReference] private IConverterFloatToBool _converter;
#else
#if ASPID_UI_SERIALIZE_REFERENCE_DROPDOWN_INTEGRATION
        [SerializeReferenceDropdown]
#endif // ASPID_UI_SERIALIZE_REFERENCE_DROPDOWN_INTEGRATION
        [SerializeReference] private IConverter<float, bool> _converter;
#endif // !UNITY_6000_0_OR_NEWER
        
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