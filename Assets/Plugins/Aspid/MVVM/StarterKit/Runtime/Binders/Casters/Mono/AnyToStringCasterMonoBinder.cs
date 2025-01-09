using UnityEngine;
using Aspid.MVVM.Mono;
using UnityEngine.Events;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("MVVM/Binders/Casters/Any To String Caster Binder")]
    public sealed class AnyToStringCasterMonoBinder : MonoBinder, IBinder<object>
    {
        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#if UNITY_2023_1_OR_NEWER
        private IConverter<object, string> _converter = new ObjectToStringConverter();
#else
        private IConverterObjectToString _converter = new ObjectToStringConverter();
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