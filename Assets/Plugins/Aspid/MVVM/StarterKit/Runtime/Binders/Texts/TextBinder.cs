#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
#nullable enable
using TMPro;
using System;
using UnityEngine;
using System.Globalization;
using Aspid.MVVM.StarterKit.Converters;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.Converters.IConverter<string?, string?>;
#else
using Converter = Aspid.MVVM.StarterKit.Converters.IConverterString;
#endif

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public class TextBinder : TargetBinder<TMP_Text>, IBinder<string?>, INumberBinder
    {
        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter? _converter;

        public TextBinder(TMP_Text target)
            : base(target) { }
        
        public TextBinder(TMP_Text target, Func<string?, string?> converter)
            : this(target, converter.ToConvert()) { }
        
        public TextBinder(TMP_Text target, Converter? converter)
            : base(target)
        {
            _converter = converter; 
        }

        public void SetValue(string? value)
        {
            if (value is null) Target.text = null;
            else Target.text = _converter?.Convert(value) ?? value;
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