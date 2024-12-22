#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public sealed class GraphicColorSwitcherBinder : SwitcherBinder<Color>
    {
        [Header("Component")]
        [SerializeField] private Graphic _graphic;

#if UNITY_2023_1_OR_NEWER
        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#endif
        private IConverter<Color, Color>? _converter;
        
        public GraphicColorSwitcherBinder(
            Color trueColor, 
            Color falseColor, 
            Graphic graphic,
            Func<Color, Color> converter)
            : this(trueColor, falseColor, graphic, converter.ToConvert()) { }
        
        public GraphicColorSwitcherBinder(
            Color trueColor, 
            Color falseColor, 
            Graphic graphic,
            IConverter<Color, Color>? converter = null)
            : base(trueColor, falseColor)
        {
            _converter = converter;
            _graphic = graphic ?? throw new ArgumentNullException(nameof(graphic));
        }

        protected override void SetValue(Color value) =>
            _graphic.color = value;
    }
}