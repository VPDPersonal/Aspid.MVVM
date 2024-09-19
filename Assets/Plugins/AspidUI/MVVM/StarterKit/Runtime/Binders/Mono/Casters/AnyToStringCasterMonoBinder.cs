using UnityEngine;
using UnityEngine.Events;
using AspidUI.MVVM.Unity;
using AspidUI.MVVM.StarterKit.Converters;
using AspidUI.MVVM.StarterKit.Converters.Strings;

namespace AspidUI.MVVM.StarterKit.Binders.Mono.Casters
{
    [AddComponentMenu("UI/Binders/Casters/Any To String Caster Binder")]
    public sealed class AnyToStringCasterMonoBinder : MonoBinder, IBinder<object>
    {
        [Header("Converter")]
#if ASPID_UI_SERIALIZE_REFERENCE_DROPDOWN_INTEGRATION
        [SerializeReferenceDropdown]
#endif
        [SerializeReference] private IConverterObjectToString _converter = new ObjectToStringConverter();
        
        [Header("Events")]
        [SerializeField] private UnityEvent<string> _casted;
        
        public void SetValue(object value)
        {
            var castedValue = _converter.Convert(value);
            _casted?.Invoke(castedValue);
        }
    }
}