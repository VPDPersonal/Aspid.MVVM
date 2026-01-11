#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
#nullable enable
using TMPro;
using System;
using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<float, float>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterFloat;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public class TextFontSizeBinder : TargetBinder<TMP_Text>, INumberBinder
    {
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter? _converter;
        
        public TextFontSizeBinder(TMP_Text target, BindMode mode)
            : this(target, converter: null, mode) { }
        
        public TextFontSizeBinder(TMP_Text target, Converter? converter = null, BindMode mode = BindMode.OneWay) 
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
            _converter = converter;
        }
        
        public void SetValue(int value) =>
            SetValue((float)value);
        
        public void SetValue(long value) =>
            SetValue((float)value);
        
        public void SetValue(float value) =>
            Target.fontSize = _converter?.Convert(value) ?? value;
        
        public void SetValue(double value) =>
            SetValue((float)value);
    }
}
#endif