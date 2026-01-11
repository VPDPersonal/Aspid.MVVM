using UnityEngine;
using UnityEngine.Events;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<float, bool>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterFloatToBool;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenuByType(typeof(bool))]
    [AddComponentMenu("Aspid/MVVM/Binders/UnityEvent/UnityEvent Binder – Number Condition")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/UnityEvent/UnityEvent Binder – Number Condition")]
    public sealed partial class UnityEventNumberConditionMonoBinder : MonoBinder, INumberBinder
    {
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;

        [SerializeField] private UnityEvent<bool> _set;
        
        [BinderLog]
        public void SetValue(int value) =>
            SetValue((float)value);

        [BinderLog]
        public void SetValue(long value) =>
            SetValue((float)value);

        [BinderLog]
        public void SetValue(float value)
        {
            if (_converter is null)
            {
                Debug.LogError($"No converter assigned to {nameof(UnityEventNumberConditionMonoBinder)}");
                return;
            }
            
            _set.Invoke(_converter.Convert(value));
        }

        [BinderLog]
        public void SetValue(double value) =>
            SetValue((float)value);
    }
}