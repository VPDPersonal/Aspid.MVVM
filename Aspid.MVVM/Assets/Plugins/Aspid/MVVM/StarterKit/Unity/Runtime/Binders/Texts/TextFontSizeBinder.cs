#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
#nullable enable
using TMPro;
using System;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<float, float>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterFloat;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public class TextFontSizeBinder : TargetFloatBinder<TMP_Text>
    {
        protected sealed override float Property
        {
            get => Target.fontSize;
            set => Target.fontSize = value;
        }
        
        public TextFontSizeBinder(TMP_Text target, BindMode mode)
            : this(target, converter: null, mode) { }
        
        public TextFontSizeBinder(TMP_Text target, Converter? converter = null, BindMode mode = BindMode.OneWay) 
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}
#endif