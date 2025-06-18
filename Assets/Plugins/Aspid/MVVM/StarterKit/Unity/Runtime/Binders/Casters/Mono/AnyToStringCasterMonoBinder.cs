using UnityEngine;
using Aspid.MVVM.Unity;
using UnityEngine.Events;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<object, string>;
#else
using Converter = Aspid.MVVM.StarterKit.Unity.IConverterObjectToString;
#endif

namespace Aspid.MVVM.StarterKit.Unity
{
    [AddPropertyContextMenu(typeof(string))]
    [AddComponentMenu("Aspid/MVVM/Binders/Casters/Any To String Caster Binder")]
    [AddComponentContextMenu(typeof(Component),"Add General Binder/Casters/Any To String Caster Binder")]
    public sealed class AnyToStringCasterMonoBinder : MonoBinder, IAnyBinder 
    {
        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter = new ObjectToStringConverter();
        
        [Header("Events")]
        [SerializeField] private UnityEvent<string> _casted;
        
        [BinderLog]
        public void SetValue<T>(T value)
        {
            var castedValue = _converter.Convert(value);
            _casted?.Invoke(castedValue);
        }
    }
}