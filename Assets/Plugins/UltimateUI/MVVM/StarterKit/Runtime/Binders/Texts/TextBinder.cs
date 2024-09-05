#if UNITY_2023_1_OR_NEWER || ULTIMATE_UI_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using System;
using System.Globalization;
using UltimateUI.MVVM.StarterKit.Converters;

namespace UltimateUI.MVVM.StarterKit.Binders.Texts
{
    public class TextBinder : Binder, IBinder<string>, INumberBinder
    {
        protected readonly TMP_Text Text;
        protected readonly IConverter<string, string> Converter;

        public TextBinder(TMP_Text text, Func<string, string> converter = null)
            : this(text, new GenericFuncConverter<string, string>(converter)) { }
        
        public TextBinder(TMP_Text text, IConverter<string, string> converter = null)
        {
            Text = text;
            Converter = converter;
        }

        public void SetValue(string value) =>
            Text.text = Converter?.Convert(value) ?? value;
        
        public void SetValue(int value) =>
            SetValue(value.ToString());
        
        public void SetValue(long value) =>
            SetValue(value.ToString());
        
        public void SetValue(float value) =>
            SetValue(value.ToString(CultureInfo.InvariantCulture));
        
        public void SetValue(double value) =>
            SetValue(value.ToString(CultureInfo.InvariantCulture));
    }
}
#endif