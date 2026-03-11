#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Color, UnityEngine.Color>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterColor;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherBinder{Graphic, Color, Converter}"/> that switches the <see cref="Graphic.color"/>
    /// property between two <see cref="Color"/> values based on the bound boolean ViewModel value.
    /// </summary>
    /// <example>
    /// Switch the Graphic color between two values based on a boolean ViewModel property.
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField]
    ///     private GraphicColorSwitcherBinder _isHighlighted;
    /// }
    ///    
    /// [ViewModel]
    /// public partial class ExampleViewModel
    /// {
    ///     [Bind] public bool _isHighlighted;
    /// }
    /// </code>
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField] private Graphic _graphic;
    ///    
    ///     private GraphicColorSwitcherBinder IsHighlighted => new(
    ///         _graphic, Color.yellow, Color.white);
    /// }
    ///    
    /// [ViewModel]
    /// public partial class ExampleViewModel
    /// {
    ///     [Bind] public bool _isHighlighted;
    /// }
    /// </code>
    /// </example>
    [Serializable]
    public sealed class GraphicColorSwitcherBinder : SwitcherBinder<Graphic, Color, Converter>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="GraphicColorSwitcherBinder"/> targeting the specified <see cref="Graphic"/>
        /// with no converter.
        /// </summary>
        /// <param name="target">The <see cref="Graphic"/> whose <see cref="Graphic.color"/> property is switched.</param>
        /// <param name="trueColor">The color assigned when the bound value is <see langword="true"/>.</param>
        /// <param name="falseColor">The color assigned when the bound value is <see langword="false"/>.</param>
        /// <param name="mode">The binding mode to use.</param>
        public GraphicColorSwitcherBinder(
            Graphic target,
            Color trueColor,
            Color falseColor,
            BindMode mode)
            : this(target, trueColor, falseColor, converter: null, mode) { }

        /// <summary>
        /// Initializes a new instance of <see cref="GraphicColorSwitcherBinder"/> targeting the specified <see cref="Graphic"/>.
        /// </summary>
        /// <param name="target">The <see cref="Graphic"/> whose <see cref="Graphic.color"/> property is switched.</param>
        /// <param name="trueColor">The color assigned when the bound value is <see langword="true"/>.</param>
        /// <param name="falseColor">The color assigned when the bound value is <see langword="false"/>.</param>
        /// <param name="converter">The converter used to transform the bound value, or <see langword="null"/> to use the value as-is.</param>
        /// <param name="mode">The binding mode to use.</param>
        public GraphicColorSwitcherBinder(
            Graphic target,
            Color trueColor,
            Color falseColor,
            Converter? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, trueColor, falseColor, converter, mode) { }

        /// <inheritdoc/>
        protected override void SetValue(Color value) =>
            Target.color = value;
    }
}