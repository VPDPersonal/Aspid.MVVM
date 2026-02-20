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
    public class GraphicColorComponentBinder : TargetFloatBinder<Graphic>
    {
        [SerializeField] private ColorComponent _colorComponent = ColorComponent.A;

        protected sealed override float Property
        {
            get => GetConvertedValue(Target.GetColorComponent(_colorComponent));
            set => Target.SetColorComponent(_colorComponent, GetConvertedValue(value));
        }

        public GraphicColorComponentBinder(Graphic target, ColorComponent colorComponent, BindMode mode)
            : this(target, colorComponent, converter: null,  mode) { }

        public GraphicColorComponentBinder(Graphic target, ColorComponent colorComponent = ColorComponent.A, Converter? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
            _colorComponent = colorComponent;
        }
    }
}