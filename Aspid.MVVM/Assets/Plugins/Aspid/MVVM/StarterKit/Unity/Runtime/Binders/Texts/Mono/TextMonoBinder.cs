#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<string, string>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterString;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Text/Text Binder – Text")]
    [AddBinderContextMenu(typeof(TMP_Text), serializePropertyNames: "m_text")]
    public partial class TextMonoBinder : ComponentMonoBinder<TMP_Text>, IBinder<string>, INumberBinder
    {
        [SerializeField] private CultureInfoMode _cultureInfoMode = CultureInfoMode.CurrentCulture;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter = new StringFormatConverter();
        
        [BinderLog]
        public void SetValue(string value) =>
            CachedComponent.text = _converter?.Convert(value) ?? value;
        
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
    }
}
#endif