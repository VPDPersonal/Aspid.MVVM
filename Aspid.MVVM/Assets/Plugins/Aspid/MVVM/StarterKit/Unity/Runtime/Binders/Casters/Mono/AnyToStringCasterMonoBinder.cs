using UnityEngine;
using UnityEngine.Events;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<object, string>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterObjectToString;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenuByType(typeof(string))]
    [AddComponentMenu("Aspid/MVVM/Binders/Casters/Any To String Caster Binder")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/Casters/Any To String Caster Binder")]
    public sealed class AnyToStringCasterMonoBinder : MonoBinder, IAnyBinder 
    {
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter = new ObjectToStringConverter();
        
        [Header("Events")]
        [SerializeField] private UnityEvent<string> _casted;

        private void OnValidate() =>
            _converter ??= new ObjectToStringConverter();

        [BinderLog]
        public void SetValue<T>(T value)
        {
            if (_converter is null)
            {
                Debug.LogError($"No converter assigned to {nameof(AnyToStringCasterMonoBinder)}");
                return;
            }
            
            _casted?.Invoke(_converter.Convert(value));
        }
    }
}