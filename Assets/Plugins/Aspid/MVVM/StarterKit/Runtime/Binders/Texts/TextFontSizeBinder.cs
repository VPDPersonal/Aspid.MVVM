#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
#nullable enable
using TMPro;
using System;
using UnityEngine;
using Aspid.MVVM.Mono.Generation;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public class TextFontSizeBinder : TargetBinder<TMP_Text>, INumberBinder
    {
#if UNITY_2023_1_OR_NEWER
        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#endif
        private IConverter<float, float>? _converter;
        
        public TextFontSizeBinder(TMP_Text target)
            : base(target) { }

        public TextFontSizeBinder(TMP_Text target, Func<float, float> converter) 
            : this(target, converter.ToConvert()) { }
        
        public TextFontSizeBinder(TMP_Text target, IConverter<float, float> converter) 
            : base(target)
        {
            _converter = converter;
        }

        [BinderLog]
        public void SetValue(int value) =>
            SetValue((float)value);

        [BinderLog]
        public void SetValue(long value) =>
            SetValue((float)value);

        [BinderLog]
        public void SetValue(float value) =>
            Target.fontSize = _converter?.Convert(value) ?? value;

        [BinderLog]
        public void SetValue(double value) =>
            SetValue((float)value);
    }
}
#endif