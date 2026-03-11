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
    /// <summary>
    /// <see cref="SwitcherBinder{Graphic, float, Converter}"/> that switches a single <see cref="ColorComponent"/>
    /// channel of the <see cref="Graphic.color"/> property between two <see cref="float"/> values
    /// based on the bound boolean ViewModel value.
    /// </summary>
    /// <example>
    /// Switch the alpha channel of a Graphic between two values based on a boolean ViewModel property.
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField]
    ///     private GraphicColorComponentSwitcherBinder _isVisible;
    /// }
    ///    
    /// [ViewModel]
    /// public partial class ExampleViewModel
    /// {
    ///     [Bind] public bool _isVisible;
    /// }
    /// </code>
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField] private Graphic _graphic;
    ///    
    ///     private GraphicColorComponentSwitcherBinder IsVisible => new(
    ///         _graphic, trueColor: 1f, falseColor: 0f, ColorComponent.A);
    /// }
    ///    
    /// [ViewModel]
    /// public partial class ExampleViewModel
    /// {
    ///     [Bind] public bool _isVisible;
    /// }
    /// </code>
    /// </example>
    [Serializable]
    public sealed class GraphicColorComponentSwitcherBinder : SwitcherBinder<Graphic, float, Converter>
    {
        [SerializeField] private ColorComponent _component = ColorComponent.A;

        /// <summary>
        /// Initializes a new instance of <see cref="GraphicColorComponentSwitcherBinder"/> targeting the specified <see cref="Graphic"/>
        /// with no converter.
        /// </summary>
        /// <param name="target">The <see cref="Graphic"/> whose color channel is switched.</param>
        /// <param name="trueColor">The channel value assigned when the bound value is <see langword="true"/>.</param>
        /// <param name="falseColor">The channel value assigned when the bound value is <see langword="false"/>.</param>
        /// <param name="component">The color channel to switch.</param>
        /// <param name="mode">The binding mode to use.</param>
        public GraphicColorComponentSwitcherBinder(
            Graphic target,
            float trueColor,
            float falseColor,
            ColorComponent component,
            BindMode mode)
            : this(target, trueColor, falseColor, component, converter: null, mode) { }

        /// <summary>
        /// Initializes a new instance of <see cref="GraphicColorComponentSwitcherBinder"/> targeting the specified <see cref="Graphic"/>.
        /// </summary>
        /// <param name="target">The <see cref="Graphic"/> whose color channel is switched.</param>
        /// <param name="trueColor">The channel value assigned when the bound value is <see langword="true"/>.</param>
        /// <param name="falseColor">The channel value assigned when the bound value is <see langword="false"/>.</param>
        /// <param name="component">The color channel to switch.</param>
        /// <param name="converter">The converter used to transform the bound float value, or <see langword="null"/> to use the value as-is.</param>
        /// <param name="mode">The binding mode to use.</param>
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

        /// <inheritdoc/>
        protected override void SetValue(float value) =>
            Target.SetColorComponent(_component, value);
    }
}