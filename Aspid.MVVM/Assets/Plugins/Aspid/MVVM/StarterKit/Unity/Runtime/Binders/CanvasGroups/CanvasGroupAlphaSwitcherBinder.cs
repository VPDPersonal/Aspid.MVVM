#nullable enable
using System;
using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<float, float>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterFloat;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherBinder{CanvasGroup, float, IConverter{float, float}}"/> that switches the <see cref="CanvasGroup.alpha"/>
    /// property between two <see cref="float"/> values based on the bound boolean ViewModel value.
    /// </summary>
    /// <remarks>
    /// The bound value is clamped to [0, 1] before being applied to <see cref="CanvasGroup.alpha"/>.
    /// </remarks>
    /// <example>
    /// Switch the CanvasGroup alpha between two values based on a boolean ViewModel property.
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField]
    ///     private CanvasGroupAlphaSwitcherBinder _isVisible;
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
    ///     [SerializeField] private CanvasGroup _canvasGroup;
    ///    
    ///     private CanvasGroupAlphaSwitcherBinder IsVisible => new(
    ///         _canvasGroup, trueValue: 1f, falseValue: 0f);
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
    public sealed class CanvasGroupAlphaSwitcherBinder : SwitcherBinder<CanvasGroup, float, Converter>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="CanvasGroupAlphaSwitcherBinder"/> targeting the specified <see cref="CanvasGroup"/>
        /// with no converter.
        /// </summary>
        /// <param name="target">The <see cref="CanvasGroup"/> whose <see cref="CanvasGroup.alpha"/> property is switched.</param>
        /// <param name="trueValue">The alpha value assigned when the bound value is <see langword="true"/>.</param>
        /// <param name="falseValue">The alpha value assigned when the bound value is <see langword="false"/>.</param>
        /// <param name="mode">The binding mode to use.</param>
        public CanvasGroupAlphaSwitcherBinder(
            CanvasGroup target,
            float trueValue,
            float falseValue,
            BindMode mode)
            : this(target, trueValue, falseValue, converter: null, mode) { }

        /// <summary>
        /// Initializes a new instance of <see cref="CanvasGroupAlphaSwitcherBinder"/> targeting the specified <see cref="CanvasGroup"/>.
        /// </summary>
        /// <param name="target">The <see cref="CanvasGroup"/> whose <see cref="CanvasGroup.alpha"/> property is switched.</param>
        /// <param name="trueValue">The alpha value assigned when the bound value is <see langword="true"/>.</param>
        /// <param name="falseValue">The alpha value assigned when the bound value is <see langword="false"/>.</param>
        /// <param name="converter">The converter used to transform the bound float value, or <see langword="null"/> to use the default.</param>
        /// <param name="mode">The binding mode to use.</param>
        public CanvasGroupAlphaSwitcherBinder(
            CanvasGroup target,
            float trueValue,
            float falseValue,
            Converter? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, converter, mode) { }

        /// <inheritdoc/>
        protected override void SetValue(float value) =>
            Target.alpha = Mathf.Clamp01(value);
    }
}