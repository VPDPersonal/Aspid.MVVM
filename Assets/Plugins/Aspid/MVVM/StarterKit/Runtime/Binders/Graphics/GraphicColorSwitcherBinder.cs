#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;
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
            BindMode mode)
            : this(target, trueColor, falseColor, null, mode) { }
        
        public GraphicColorSwitcherBinder(
            Graphic target,
            Color trueColor, 
            Color falseColor, 
            Converter? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, trueColor, falseColor, mode)
        {
            _converter = converter;
        }

        protected override void SetValue(Color value) =>
            Target.color = _converter?.Convert(value) ?? value;
    }
}