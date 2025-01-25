#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
#nullable enable
using TMPro;
using System;
using UnityEngine;
using Aspid.MVVM.Mono.Generation;
using Aspid.MVVM.StarterKit.Converters;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.Converters.IConverter<float, float>;
#else
using Converter = Aspid.MVVM.StarterKit.Converters.IConverterFloat;
#endif

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public class TextFontSizeBinder : TargetBinder<TMP_Text>, INumberBinder
    {
        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter? _converter;
        
        public TextFontSizeBinder(TMP_Text target)
            : base(target) { }

        public TextFontSizeBinder(TMP_Text target, Func<float, float> converter) 
            : this(target, converter.ToConvert()) { }
        
        public TextFontSizeBinder(TMP_Text target, Converter converter) 
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