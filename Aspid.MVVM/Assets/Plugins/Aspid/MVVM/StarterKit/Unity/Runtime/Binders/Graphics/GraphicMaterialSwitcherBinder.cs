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
    /// <see cref="SwitcherBinder{Graphic, Material, Converter}"/> that switches the <see cref="Graphic.material"/>
    /// property between two <see cref="Material"/> values based on the bound boolean ViewModel value.
    /// </summary>
    /// <example>
    /// Switch the Graphic material between two values based on a boolean ViewModel property.
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField]
    ///     private GraphicMaterialSwitcherBinder _isAlternate;
    /// }
    ///    
    /// [ViewModel]
    /// public partial class ExampleViewModel
    /// {
    ///     [Bind] public bool _isAlternate;
    /// }
    /// </code>
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField] private RawImage _image;
    ///     [SerializeField] private Material _alternateMaterial;
    ///     [SerializeField] private Material _defaultMaterial;
    ///    
    ///     private GraphicMaterialSwitcherBinder IsAlternate => new(
    ///         _image, _alternateMaterial, _defaultMaterial);
    /// }
    ///    
    /// [ViewModel]
    /// public partial class ExampleViewModel
    /// {
    ///     [Bind] public bool _isAlternate;
    /// }
    /// </code>
    /// </example>
    [Serializable]
    public sealed class GraphicMaterialSwitcherBinder : SwitcherBinder<Graphic, Material, Converter>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="GraphicMaterialSwitcherBinder"/> targeting the specified <see cref="RawImage"/>
        /// with no converter.
        /// </summary>
        /// <param name="target">The <see cref="RawImage"/> whose <see cref="Graphic.material"/> property is switched.</param>
        /// <param name="trueValue">The material assigned when the bound value is <see langword="true"/>.</param>
        /// <param name="falseValue">The material assigned when the bound value is <see langword="false"/>.</param>
        /// <param name="mode">The binding mode to use.</param>
        public GraphicMaterialSwitcherBinder(
            RawImage target,
            Material trueValue,
            Material falseValue,
            BindMode mode = BindMode.OneWay)
            : this(target, trueValue, falseValue, converter: null, mode) { }

        /// <summary>
        /// Initializes a new instance of <see cref="GraphicMaterialSwitcherBinder"/> targeting the specified <see cref="RawImage"/>.
        /// </summary>
        /// <param name="target">The <see cref="RawImage"/> whose <see cref="Graphic.material"/> property is switched.</param>
        /// <param name="trueValue">The material assigned when the bound value is <see langword="true"/>.</param>
        /// <param name="falseValue">The material assigned when the bound value is <see langword="false"/>.</param>
        /// <param name="converter">The converter used to transform the bound value, or <see langword="null"/> to use the value as-is.</param>
        /// <param name="mode">The binding mode to use.</param>
        public GraphicMaterialSwitcherBinder(
            RawImage target,
            Material trueValue,
            Material falseValue,
            Converter? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, converter, mode) { }

        /// <inheritdoc/>
        protected override void SetValue(Material? value) =>
            Target.material = value;
    }
}