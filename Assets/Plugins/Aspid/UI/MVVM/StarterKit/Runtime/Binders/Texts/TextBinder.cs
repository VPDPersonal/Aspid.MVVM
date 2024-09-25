#if UNITY_2023_1_OR_NEWER || ASPID_UI_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using System;
using System.Globalization;
using Aspid.UI.MVVM.StarterKit.Converters;

namespace Aspid.UI.MVVM.StarterKit.Binders.Texts
{
    public class TextBinder : Binder, IBinder<string>, INumberBinder
    {
        protected readonly TMP_Text Text;
        protected readonly IConverter<string, string> Converter;

        public TextBinder(TMP_Text text)
        {
            Text = text;
            Converter = null;
        }
        
        public TextBinder(TMP_Text text, Func<string, string> converter)
            : this(text, new GenericFuncConverter<string, string>(converter)) { }
        
        public TextBinder(TMP_Text text, IConverter<string, string> converter)
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