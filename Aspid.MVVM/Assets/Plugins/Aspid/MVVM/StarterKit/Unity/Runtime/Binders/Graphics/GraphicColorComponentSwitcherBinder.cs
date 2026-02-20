#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<float, float>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterFloat;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public sealed class GraphicColorComponentSwitcherBinder : SwitcherBinder<Graphic, float, Converter>
    {
        [SerializeField] private ColorComponent _component = ColorComponent.A;
        
        public GraphicColorComponentSwitcherBinder(
            Graphic target,
            float trueColor, 
            float falseColor,
            ColorComponent component,
            BindMode mode)
            : this(target, trueColor, falseColor, component, converter: null, mode) { }
        
        public GraphicColorComponentSwitcherBinder(
            Graphic target,
            float trueColor, 
            float falseColor,
            ColorComponent component = ColorComponent.A,
            Converter? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, trueColor, falseColor, converter, mode)
        {
            _component = component;
        }

        protected override void SetValue(float value) =>
            Target.SetColorComponent(_component, value);
    }
}