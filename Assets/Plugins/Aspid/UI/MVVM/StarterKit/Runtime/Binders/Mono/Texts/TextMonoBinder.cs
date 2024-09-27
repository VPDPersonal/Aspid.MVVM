#if UNITY_2023_1_OR_NEWER || ASPID_UI_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;
using System.Globalization;
using Aspid.UI.MVVM.Mono.Generation;
using Aspid.UI.MVVM.StarterKit.Converters;
using Aspid.UI.MVVM.StarterKit.Converters.Strings;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono.Texts
{
    [AddComponentMenu("UI/Binders/Text/Text Binder")]
    public partial class TextMonoBinder : ComponentMonoBinder<TMP_Text>, IBinder<string>, INumberBinder
    {
        [field: Header("Converter")]
        [field: SerializeReference]
#if ASPID_UI_SERIALIZE_REFERENCE_DROPDOWN_INTEGRATION
        [field: SerializeReferenceDropdown]
#endif
        protected IConverterStringToString Converter { get; private set; } = new StringFormatConverter();
        
        [BinderLog]
        public void SetValue(string value) =>
            CachedComponent.text = Converter?.Convert(value) ?? value;
        
        [BinderLog]
        public void SetValue(int value) =>
            SetValue(value.ToString());
                
        [BinderLog]
        public void SetValue(long value) =>
            SetValue(value.ToString());
        
        [BinderLog]
        public void SetValue(float value) =>
            SetValue(value.ToString(CultureInfo.InvariantCulture));
                
        [BinderLog]
        public void SetValue(double value) =>
            SetValue(value.ToString(CultureInfo.InvariantCulture));
    }
}
#endif