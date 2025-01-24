using UnityEngine;
using Aspid.MVVM.Mono;
using UnityEngine.Events;
using Aspid.MVVM.StarterKit.Converters;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.Converters.IConverter<object, string>;
#else
using Converter = Aspid.MVVM.StarterKit.Converters.IConverterObjectToString;
#endif

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Aspid/MVVM/Binders/Casters/Any To String Caster Binder")]
    public sealed class AnyToStringCasterMonoBinder : MonoBinder, IBinder<object>
    {
        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter = new ObjectToStringConverter();
        
        [Header("Events")]
        [SerializeField] private UnityEvent<string> _casted;
        
        public void SetValue(object value)
        {
            var castedValue = _converter.Convert(value);
            _casted?.Invoke(castedValue);
        }
    }
}