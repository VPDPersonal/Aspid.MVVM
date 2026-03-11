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
    /// <see cref="SwitcherBinder{AudioSource, float, IConverter{float, float}}"/> that switches the <see cref="AudioSource.spread"/>
    /// property between two <see cref="float"/> values based on the bound boolean ViewModel value.
    /// </summary>
    /// <remarks>
    /// The bound value is clamped to [0, 360] before being applied to <see cref="AudioSource.spread"/>.
    /// </remarks>
    /// <example>
    /// Switch the AudioSource spread between two values based on a boolean ViewModel property.
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField]
    ///     private AudioSourceSpreadSwitcherBinder _isHighSpread;
    /// }
    ///    
    /// [ViewModel]
    /// public partial class ExampleViewModel
    /// {
    ///     [Bind] public bool _isHighSpread;
    /// }
    /// </code>
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField] private AudioSource _audioSource;
    ///     [SerializeField] private float _highSpread;
    ///     [SerializeField] private float _lowSpread;
    ///    
    ///     private AudioSourceSpreadSwitcherBinder IsHighSpread => new(
    ///         _audioSource, _highSpread, _lowSpread);
    /// }
    ///    
    /// [ViewModel]
    /// public partial class ExampleViewModel
    /// {
    ///     [Bind] public bool _isHighSpread;
    /// }
    /// </code>
    /// </example>
    [Serializable]
    public sealed class AudioSourceSpreadSwitcherBinder : SwitcherBinder<AudioSource, float, Converter>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="AudioSourceSpreadSwitcherBinder"/> targeting the specified <see cref="AudioSource"/>
        /// with no converter.
        /// </summary>
        /// <param name="target">The <see cref="AudioSource"/> whose <see cref="AudioSource.spread"/> property is switched.</param>
        /// <param name="trueValue">The spread assigned when the bound value is <see langword="true"/>.</param>
        /// <param name="falseValue">The spread assigned when the bound value is <see langword="false"/>.</param>
        /// <param name="mode">The binding mode to use.</param>
        public AudioSourceSpreadSwitcherBinder(
            AudioSource target,
            float trueValue,
            float falseValue,
            BindMode mode)
            : this(target, trueValue, falseValue, converter: null, mode) { }

        /// <summary>
        /// Initializes a new instance of <see cref="AudioSourceSpreadSwitcherBinder"/> targeting the specified <see cref="AudioSource"/>.
        /// </summary>
        /// <param name="target">The <see cref="AudioSource"/> whose <see cref="AudioSource.spread"/> property is switched.</param>
        /// <param name="trueValue">The spread assigned when the bound value is <see langword="true"/>.</param>
        /// <param name="falseValue">The spread assigned when the bound value is <see langword="false"/>.</param>
        /// <param name="converter">The converter used to transform the bound float value, or <see langword="null"/> to use the default.</param>
        /// <param name="mode">The binding mode to use.</param>
        public AudioSourceSpreadSwitcherBinder(
            AudioSource target,
            float trueValue,
            float falseValue,
            Converter? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, converter, mode) { }

        /// <inheritdoc/>
        protected override void SetValue(float value) =>
            Target.spread = value;

        /// <summary>
        /// Called when converting the selected value before applying it to the <see cref="AudioSource.spread"/> property.
        /// Clamps the converted value to the valid range of 0 to 360.
        /// </summary>
        protected override float GetConvertedValue(float value) =>
            Mathf.Clamp(base.GetConvertedValue(value), min: 0, max: 360);
    }
}