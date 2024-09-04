using UnityEngine;
using UnityEngine.Events;
using UltimateUI.MVVM.Unity;
using UltimateUI.MVVM.Unity.Generation;
using UltimateUI.MVVM.StarterKit.Converters;
using UltimateUI.MVVM.StarterKit.Converters.Strings;

namespace UltimateUI.MVVM.StarterKit.Binders.Unity.Casters
{
    [AddComponentMenu("UI/Binders/Casters/String Empty To Bool Caster Binder")]
    public sealed partial class StringToBoolCasterMonoBinder : MonoBinder, IBinder<string>
    {
        [Header("Converter")]
        [SerializeReference] private IConverter<string, bool> _converter = new StringEmptyToBoolConverter();
        
        [Header("Events")]
        [SerializeField] private UnityEvent<bool> _casted;
        
        [BinderLog]
        public void SetValue(string value) =>
            _casted?.Invoke(_converter.Convert(value));
    }

}