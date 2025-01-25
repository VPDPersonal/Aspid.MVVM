#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;
using Aspid.MVVM.StarterKit.Converters;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.Converters.IConverter<UnityEngine.Color, UnityEngine.Color>;
#else
using Converter = Aspid.MVVM.StarterKit.Converters.IConverterColor;
#endif

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public sealed class GraphicColorSwitcherBinder : SwitcherBinder<Graphic, Color>
    {
        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter? _converter;
        
        public GraphicColorSwitcherBinder(
            Graphic target,
            Color trueColor, 
            Color falseColor, 
            Func<Color, Color> converter)
            : this(target, trueColor, falseColor, converter.ToConvert()) { }
        
        public GraphicColorSwitcherBinder(
            Graphic target,
            Color trueColor, 
            Color falseColor, 
            Converter? converter = null)
            : base(target, trueColor, falseColor)
        {
            _converter = converter;
        }

        protected override void SetValue(Color value) =>
            Target.color = _converter?.Convert(value) ?? value;
    }
}