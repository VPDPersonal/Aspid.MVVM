using UnityEngine;
using UnityEngine.Events;
using AspidUI.MVVM.Unity;
using AspidUI.MVVM.Unity.Generation;
using AspidUI.MVVM.StarterKit.Converters;
using AspidUI.MVVM.StarterKit.Converters.Bools;

namespace AspidUI.MVVM.StarterKit.Binders.Mono.Casters
{
    [AddComponentMenu("UI/Binders/Casters/String Empty To Bool Caster Binder")]
    public sealed partial class StringToBoolCasterMonoBinder : MonoBinder, IBinder<string>
    {
        [Header("Converter")]
#if ASPID_UI_SERIALIZE_REFERENCE_DROPDOWN_INTEGRATION
        [SerializeReferenceDropdown]
#endif
        [SerializeReference] private IConverterStringToBool _converter = new StringEmptyToBoolConverter();
        
        [Header("Events")]
        [SerializeField] private UnityEvent<bool> _casted;
        
        [BinderLog]
        public void SetValue(string value) =>
            _casted?.Invoke(_converter.Convert(value));
    }
}