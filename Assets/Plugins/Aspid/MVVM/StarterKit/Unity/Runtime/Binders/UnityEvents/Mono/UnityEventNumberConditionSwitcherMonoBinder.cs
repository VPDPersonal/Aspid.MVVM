using UnityEngine;
using Aspid.MVVM.Unity;
using UnityEngine.Events;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<float, bool>;
#else
using Converter = Aspid.MVVM.StarterKit.Unity.IConverterFloatToBool;
#endif

namespace Aspid.MVVM.StarterKit.Unity
{
    [AddComponentMenu("Aspid/MVVM/Binders/UnityEvent/UnityEvent Binder - Number Condition Switcher")]
    public sealed partial class UnityEventNumberConditionSwitcherMonoBinder : MonoBinder, INumberBinder
    {
        public event UnityAction TrueSet
        {
            add => _trueSet.AddListener(value);
            remove => _trueSet.RemoveListener(value);
        }
        
        public event UnityAction FalseSet
        {
            add => _falseSet.AddListener(value);
            remove => _falseSet.RemoveListener(value);
        }
        
        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;
        
        [Header("Events")]
        [SerializeField] private UnityEvent _trueSet;
        [SerializeField] private UnityEvent _falseSet;
        
        [BinderLog]
        public void SetValue(int value) =>
            SetValue((float)value);

        [BinderLog]
        public void SetValue(long value) =>
            SetValue((float)value);

        [BinderLog]
        public void SetValue(float value)
        {
            if (_converter.Convert(value)) _trueSet?.Invoke();
            else _falseSet?.Invoke();
        }

        [BinderLog]
        public void SetValue(double value) =>
            SetValue((float)value);
    }
}