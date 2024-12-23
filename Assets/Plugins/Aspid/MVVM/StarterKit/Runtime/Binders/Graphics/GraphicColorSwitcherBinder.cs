#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public sealed class GraphicColorSwitcherBinder : SwitcherBinder<Graphic, Color>
    {
#if UNITY_2023_1_OR_NEWER
        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#endif
        private IConverter<Color, Color>? _converter;
        
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
            IConverter<Color, Color>? converter = null)
            : base(target, trueColor, falseColor)
        {
            _converter = converter;
        }

        protected override void SetValue(Color value) =>
            Target.color = _converter?.Convert(value) ?? value;
    }
}