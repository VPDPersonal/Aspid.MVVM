#if ULTIMATE_UI_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;
using System.Globalization;
using UltimateUI.MVVM.Unity.Generation;
using UltimateUI.MVVM.StarterKit.Converters;
using UltimateUI.MVVM.StarterKit.Converters.Strings;

namespace UltimateUI.MVVM.StarterKit.Binders.Unity.Texts
{
    [AddComponentMenu("UI/Binders/Text/Text Binder")]
    public partial class TextMonoBinder : ComponentMonoBinder<TMP_Text>, IBinder<string>, INumberBinder
    {
        [field: Header("Converter")]
        [field: SerializeReference]
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