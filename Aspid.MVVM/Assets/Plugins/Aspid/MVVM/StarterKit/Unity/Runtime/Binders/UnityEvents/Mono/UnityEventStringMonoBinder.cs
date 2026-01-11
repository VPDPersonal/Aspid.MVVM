using UnityEngine;
using UnityEngine.Events;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<string, string>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterString;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenuByType(typeof(string))]
    [AddComponentMenu("Aspid/MVVM/Binders/UnityEvent/UnityEvent Binder – String")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/UnityEvent/UnityEvent Binder – String")]
    public sealed partial class UnityEventStringMonoBinder : MonoBinder, IBinder<string>, IAnyBinder, INumberBinder
    {
        [SerializeField] private CultureInfoMode _cultureInfoMode = CultureInfoMode.CurrentCulture;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;
        
        [SerializeField] private UnityEvent<string> _set;
        
        [BinderLog]
        public void SetValue(string value) =>
            _set?.Invoke(_converter?.Convert(value) ?? value);

        [BinderLog]
        public void SetValue(int value) =>
            SetValue(value.ToCultureString(_cultureInfoMode));
                
        [BinderLog]
        public void SetValue(long value) =>
            SetValue(value.ToCultureString(_cultureInfoMode));
        
        [BinderLog]
        public void SetValue(float value) =>
            SetValue(value.ToCultureString(_cultureInfoMode));
                
        [BinderLog]
        public void SetValue(double value) =>
            SetValue(value.ToCultureString(_cultureInfoMode));
        
        [BinderLog]
        public void SetValue<T>(T value) =>
            SetValue(value.ToString());
    }
}