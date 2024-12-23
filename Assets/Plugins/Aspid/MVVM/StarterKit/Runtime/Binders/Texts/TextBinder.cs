#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
#nullable enable
using TMPro;
using System;
using UnityEngine;
using System.Globalization;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public class TextBinder : TargetBinder<TMP_Text>, IBinder<string?>, INumberBinder
    {
#if UNITY_2023_1_OR_NEWER
        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#endif
        private IConverter<string?, string?>? _converter;

        public TextBinder(TMP_Text target)
            : base(target) { }
        
        public TextBinder(TMP_Text target, Func<string?, string?> converter)
            : this(target, converter.ToConvert()) { }
        
        public TextBinder(TMP_Text target, IConverter<string?, string?>? converter)
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