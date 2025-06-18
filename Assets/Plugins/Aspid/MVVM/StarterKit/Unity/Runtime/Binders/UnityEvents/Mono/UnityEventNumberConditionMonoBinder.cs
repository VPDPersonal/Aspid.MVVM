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
    [AddPropertyContextMenu(typeof(bool))]
    [AddComponentMenu("Aspid/MVVM/Binders/UnityEvent/UnityEvent Binder - Number Condition")]
    [AddComponentContextMenu(typeof(Component),"Add General Binder/UnityEvent/UnityEvent Binder - Number Condition")]
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