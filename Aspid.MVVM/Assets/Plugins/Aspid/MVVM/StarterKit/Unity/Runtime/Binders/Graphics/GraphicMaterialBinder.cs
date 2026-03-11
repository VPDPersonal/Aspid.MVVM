#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Material?, UnityEngine.Material?>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterMaterial;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{Graphic, Material, Converter}"/> that sets the <see cref="Graphic.material"/> property.
    /// </summary>
    /// <example>
    /// Set the Graphic material based on a Material ViewModel value.
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField]
    ///     private GraphicMaterialBinder _material;
    /// }
    ///    
    /// [ViewModel]
    /// public partial class ExampleViewModel
    /// {
    ///     [Bind] public Material _material;
    /// }
    /// </code>
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField] private RawImage _image;
    ///    
    ///     private GraphicMaterialBinder Material =>
    ///         new(_image);
    /// }
    ///    
    /// [ViewModel]
    /// public partial class ExampleViewModel
    /// {
    ///     [Bind] public Material _material;
    /// }
    /// </code>
    /// </example>
    [Serializable]
    public class GraphicMaterialBinder : TargetBinder<Graphic, Material, Converter>
    {
        /// <inheritdoc/>
        protected sealed override Material? Property
        {
            get => Target.material;
            set => Target.material = value;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="GraphicMaterialBinder"/> targeting the specified <see cref="RawImage"/>
        /// with no converter.
        /// </summary>
        /// <param name="target">The <see cref="RawImage"/> whose <see cref="Graphic.material"/> property is bound.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public GraphicMaterialBinder(RawImage target, BindMode mode = BindMode.OneWay)
            : this(target, converter: null, mode) { }

        /// <summary>
        /// Initializes a new instance of <see cref="GraphicMaterialBinder"/> targeting the specified <see cref="RawImage"/>.
        /// </summary>
        /// <param name="target">The <see cref="RawImage"/> whose <see cref="Graphic.material"/> property is bound.</param>
        /// <param name="converter">The converter used to transform the bound value, or <see langword="null"/> to use the value as-is.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public GraphicMaterialBinder(RawImage target, Converter? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}