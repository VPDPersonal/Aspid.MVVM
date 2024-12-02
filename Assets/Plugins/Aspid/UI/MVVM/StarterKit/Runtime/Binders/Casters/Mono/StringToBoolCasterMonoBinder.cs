using UnityEngine;
using Aspid.UI.MVVM.Mono;
using UnityEngine.Events;
using Aspid.UI.MVVM.Mono.Generation;
using Aspid.UI.MVVM.StarterKit.Converters;
using Aspid.UI.MVVM.StarterKit.Converters.Bools;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("UI/Binders/Casters/String To Bool Caster Binder")]
    public sealed partial class StringToBoolCasterMonoBinder : MonoBinder, IBinder<string>
    {
        [Header("Converter")]
#if ASPID_UI_SERIALIZE_REFERENCE_DROPDOWN_INTEGRATION
        [SerializeReferenceDropdown]
#endif
#if UNITY_2023_1_OR_NEWER
        [SerializeReference] private IConverter<string, bool> _converter = new StringEmptyToBoolConverter();
#else
        [SerializeReference] private IConverterStringToBool _converter = new StringEmptyToBoolConverter();
#endif
        
        [Header("Events")]
        [SerializeField] private UnityEvent<bool> _casted;
        
        [BinderLog]
        public void SetValue(string value) =>
            _casted?.Invoke(_converter.Convert(value));
    }
}