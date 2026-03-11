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
    /// <see cref="TargetColorBinder{Graphic}"/> that sets the <see cref="Graphic.color"/> property.
    /// </summary>
    /// <example>
    /// Set the Graphic color based on a Color ViewModel value.
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField]
    ///     private GraphicColorBinder _color;
    /// }
    ///    
    /// [ViewModel]
    /// public partial class ExampleViewModel
    /// {
    ///     [Bind] public Color _color;
    /// }
    /// </code>
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField] private Graphic _graphic;
    ///    
    ///     private GraphicColorBinder Color =>
    ///         new(_graphic);
    /// }
    ///    
    /// [ViewModel]
    /// public partial class ExampleViewModel
    /// {
    ///     [Bind] public Color _color;
    /// }
    /// </code>
    /// </example>
    [Serializable]
    public class GraphicColorBinder : TargetColorBinder<Graphic>
    {
        /// <inheritdoc/>
        protected sealed override Color Property
        {
            get => Target.color;
            set => Target.color = value;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="GraphicColorBinder"/> targeting the specified <see cref="Graphic"/>
        /// with no converter.
        /// </summary>
        /// <param name="target">The <see cref="Graphic"/> whose <see cref="Graphic.color"/> property is bound.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public GraphicColorBinder(Graphic target, BindMode mode)
            : this(target, converter: null, mode) { }

        /// <summary>
        /// Initializes a new instance of <see cref="GraphicColorBinder"/> targeting the specified <see cref="Graphic"/>.
        /// </summary>
        /// <param name="target">The <see cref="Graphic"/> whose <see cref="Graphic.color"/> property is bound.</param>
        /// <param name="converter">The converter used to transform the bound value, or <see langword="null"/> to use the value as-is.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public GraphicColorBinder(Graphic target, Converter? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}