using UnityEngine;
using UnityEngine.Events;
using AspidUI.MVVM.Unity;
using AspidUI.MVVM.Unity.Generation;
using AspidUI.MVVM.StarterKit.Converters;

namespace AspidUI.MVVM.StarterKit.Binders.Mono.Casters
{
#if UNITY_2023_1_OR_NEWER
    public abstract partial class GenericToStringCasterMonoBinder<T> : MonoBinder, IBinder<T>
    {
        [Header("Converter")]
#if ASPID_UI_SERIALIZE_REFERENCE_DROPDOWN_INTEGRATION
        [SerializeReferenceDropdown]
#endif
        [SerializeReference] private IConverter<T, string> _converter;
        
        [Header("Events")]
        [SerializeField] private UnityEvent<string> _casted;
        
        [BinderLog]
        public void SetValue(T value) =>
            _casted?.Invoke(_converter.Convert(value));
    }
#else
    public abstract partial class GenericToStringCasterMonoBinder<T> : MonoBinder, IBinder<T>
    {
        [Header("Events")]
        [SerializeField] private UnityEvent<string> _casted;
        
        protected abstract IConverter<T, string> Converter { get; }
        
        [BinderLog]
        public void SetValue(T value) =>
            _casted?.Invoke(Converter.Convert(value));
    }
#endif
}