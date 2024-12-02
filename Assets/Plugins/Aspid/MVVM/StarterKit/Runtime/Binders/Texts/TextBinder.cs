#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
#nullable enable
using TMPro;
using System;
using System.Globalization;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders
{
    public class TextBinder : Binder, IBinder<string?>, INumberBinder
    {
        private readonly TMP_Text _text;
        private readonly IConverter<string, string>? _converter;

        public TextBinder(TMP_Text text)
        {
            _converter = null;
            _text = text ?? throw new ArgumentNullException(nameof(text));
        }
        
        public TextBinder(TMP_Text text, Func<string, string> converter)
            : this(text, new GenericFuncConverter<string, string>(converter)) { }
        
        public TextBinder(TMP_Text text, IConverter<string, string>? converter)
        {
            _converter = converter;
            _text = text ?? throw new ArgumentNullException(nameof(text));
        }

        public void SetValue(string? value)
        {
            if (value is null) _text.text = null;
            else _text.text = _converter?.Convert(value) ?? value;
        }
        
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