using UnityEngine;
using UnityEngine.Events;
using Aspid.UI.MVVM.Mono;
using Aspid.UI.MVVM.StarterKit.Converters;
using Aspid.UI.MVVM.StarterKit.Converters.Strings;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("UI/Binders/Casters/Any To String Caster Binder")]
    public sealed class AnyToStringCasterMonoBinder : MonoBinder, IBinder<object>
    {
        [Header("Converter")]
#if ASPID_UI_SERIALIZE_REFERENCE_DROPDOWN_INTEGRATION
        [SerializeReferenceDropdown]
#endif
#if UNITY_2023_1_OR_NEWER
        [SerializeReference] private IConverter<object, string> _converter = new ObjectToStringConverter();
#else
        [SerializeReference] private IConverterObjectToString _converter = new ObjectToStringConverter();
#endif
        
        [Header("Events")]
        [SerializeField] private UnityEvent<string> _casted;
        
        public void SetValue(object value)
        {
            var castedValue = _converter.Convert(value);
            _casted?.Invoke(castedValue);
        }
    }
}