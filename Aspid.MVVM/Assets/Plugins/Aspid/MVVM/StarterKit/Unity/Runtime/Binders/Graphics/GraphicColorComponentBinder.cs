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
    /// <see cref="TargetFloatBinder{Graphic}"/> that sets a single <see cref="ColorComponent"/> channel
    /// of the <see cref="Graphic.color"/> property.
    /// </summary>
    /// <example>
    /// Set the alpha channel of a Graphic based on a float ViewModel value.
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField]
    ///     private GraphicColorComponentBinder _alpha;
    /// }
    ///    
    /// [ViewModel]
    /// public partial class ExampleViewModel
    /// {
    ///     [Bind] public float _alpha;
    /// }
    /// </code>
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField] private Graphic _graphic;
    ///    
    ///     private GraphicColorComponentBinder Alpha =>
    ///         new(_graphic, ColorComponent.A);
    /// }
    ///    
    /// [ViewModel]
    /// public partial class ExampleViewModel
    /// {
    ///     [Bind] public float _alpha;
    /// }
    /// </code>
    /// </example>
    [Serializable]
    public class GraphicColorComponentBinder : TargetFloatBinder<Graphic>
    {
        [SerializeField] private ColorComponent _colorComponent = ColorComponent.A;

        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => GetConvertedValue(Target.GetColorComponent(_colorComponent));
            set => Target.SetColorComponent(_colorComponent, GetConvertedValue(value));
        }

        /// <summary>
        /// Initializes a new instance of <see cref="GraphicColorComponentBinder"/> targeting the specified <see cref="Graphic"/>
        /// with no converter.
        /// </summary>
        /// <param name="target">The <see cref="Graphic"/> whose color channel is bound.</param>
        /// <param name="colorComponent">The color channel to bind.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public GraphicColorComponentBinder(Graphic target, ColorComponent colorComponent, BindMode mode)
            : this(target, colorComponent, converter: null, mode) { }

        /// <summary>
        /// Initializes a new instance of <see cref="GraphicColorComponentBinder"/> targeting the specified <see cref="Graphic"/>.
        /// </summary>
        /// <param name="target">The <see cref="Graphic"/> whose color channel is bound.</param>
        /// <param name="colorComponent">The color channel to bind.</param>
        /// <param name="converter">The converter used to transform the bound float value, or <see langword="null"/> to use the value as-is.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public GraphicColorComponentBinder(Graphic target, ColorComponent colorComponent = ColorComponent.A, Converter? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
            _colorComponent = colorComponent;
        }
    }
}