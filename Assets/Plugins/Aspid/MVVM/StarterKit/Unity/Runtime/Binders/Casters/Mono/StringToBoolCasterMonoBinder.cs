using UnityEngine;
using UnityEngine.Events;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<string, bool>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterStringToBool;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddPropertyContextMenu(typeof(bool))]
    [AddComponentMenu("Aspid/MVVM/Binders/Casters/String To Bool Caster Binder")]
    [AddComponentContextMenu(typeof(Component),"Add General Binder/Casters/String To Bool Caster Binder")]
    public sealed partial class StringToBoolCasterMonoBinder : MonoBinder, IBinder<string>
    {
        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter = new StringEmptyToBoolConverter();
        
        [Header("Events")]
        [SerializeField] private UnityEvent<bool> _casted;
        
        [BinderLog]
        public void SetValue(string value) =>
            _casted?.Invoke(_converter.Convert(value));
    }
}