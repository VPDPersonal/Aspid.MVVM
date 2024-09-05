#if UNITY_2023_1_OR_NEWER
using UnityEngine;
using UnityEngine.Events;
using UltimateUI.MVVM.Unity;
using UltimateUI.MVVM.Unity.Generation;
using UltimateUI.MVVM.StarterKit.Converters;

namespace UltimateUI.MVVM.StarterKit.Binders.Unity.Casters
{
    public abstract partial class GenericToStringCasterMonoBinder<T> : MonoBinder, IBinder<T>
    {
        [Header("Converter")]
#if ULTIMATE_UI_SERIALIZE_REFERENCE_DROPDOWN_INTEGRATION
        [SerializeReferenceDropdown]
#endif
        [SerializeReference] private IConverter<T, string> _converter;
        
        [Header("Events")]
        [SerializeField] private UnityEvent<string> _casted;
        
        [BinderLog]
        public void SetValue(T value) =>
            _casted?.Invoke(_converter.Convert(value));
    }
}
#endif