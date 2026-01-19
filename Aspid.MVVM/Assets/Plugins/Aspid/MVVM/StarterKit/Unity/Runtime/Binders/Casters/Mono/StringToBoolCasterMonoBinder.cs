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
    [AddBinderContextMenuByType(typeof(bool))]
    [AddComponentMenu("Aspid/MVVM/Binders/Casters/String To Bool Caster Binder")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/Casters/String To Bool Caster Binder")]
    public sealed partial class StringToBoolCasterMonoBinder : MonoBinder, IBinder<string>
    {
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter = new StringEmptyToBoolConverter();
        
        [SerializeField] private UnityEvent<bool> _casted;
        
        private void OnValidate() =>
            _converter ??= new StringEmptyToBoolConverter();
        
        [BinderLog]
        public void SetValue(string value)
        {
            if (_converter is null)
            {
                Debug.LogError($"No converter assigned to {nameof(StringToBoolCasterMonoBinder)}", context: this);
                return;
            }
            
            _casted?.Invoke(_converter.Convert(value));
        }
    }
}