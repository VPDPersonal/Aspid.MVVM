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
    /// <see cref="TargetFloatBinder{CanvasGroup}"/> that sets the <see cref="CanvasGroup.alpha"/> property.
    /// </summary>
    /// <remarks>
    /// The bound value is clamped to [0, 1] before being applied to <see cref="CanvasGroup.alpha"/>.
    /// </remarks>
    /// <example>
    /// Set the CanvasGroup alpha based on a float ViewModel value.
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField]
    ///     private CanvasGroupAlphaBinder _alpha;
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
    ///     [SerializeField] private CanvasGroup _canvasGroup;
    ///    
    ///     private CanvasGroupAlphaBinder Alpha =>
    ///         new(_canvasGroup);
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
    public class CanvasGroupAlphaBinder : TargetFloatBinder<CanvasGroup>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => Target.alpha;
            set => Target.alpha = value;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="CanvasGroupAlphaBinder"/> targeting the specified <see cref="CanvasGroup"/>
        /// with no converter.
        /// </summary>
        /// <param name="target">The <see cref="CanvasGroup"/> whose <see cref="CanvasGroup.alpha"/> property is bound.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public CanvasGroupAlphaBinder(CanvasGroup target, BindMode mode)
            : this(target, converter: null, mode) { }

        /// <summary>
        /// Initializes a new instance of <see cref="CanvasGroupAlphaBinder"/> targeting the specified <see cref="CanvasGroup"/>.
        /// </summary>
        /// <param name="target">The <see cref="CanvasGroup"/> whose <see cref="CanvasGroup.alpha"/> property is bound.</param>
        /// <param name="converter">The converter used to transform the bound float value, or <see langword="null"/> to use the default.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public CanvasGroupAlphaBinder(CanvasGroup target, Converter? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }

        /// <summary>
        /// Called when converting the bound value before applying it to the <see cref="CanvasGroup.alpha"/> property.
        /// Clamps the converted value to the valid range of 0 to 1.
        /// </summary>
        /// <remarks>
        /// When overriding this method, always call <c>base.GetConvertedValue(value)</c> to preserve
        /// the clamping behavior.
        /// </remarks>
        protected override float GetConvertedValue(float value) =>
            Mathf.Clamp01(base.GetConvertedValue(value));
    }
}