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
    /// <see cref="SwitcherBinder{AudioSource, float, IConverter{float, float}}"/> that switches the <see cref="AudioSource.reverbZoneMix"/>
    /// property between two <see cref="float"/> values based on the bound boolean ViewModel value.
    /// </summary>
    /// <remarks>
    /// The bound value is clamped to [0, 1.1] before being applied to <see cref="AudioSource.reverbZoneMix"/>.
    /// </remarks>
    /// <example>
    /// Switch the AudioSource reverbZoneMix between two values based on a boolean ViewModel property.
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField]
    ///     private AudioSourceReverbZoneMixSwitcherBinder _isHighReverbMix;
    /// }
    ///    
    /// [ViewModel]
    /// public partial class ExampleViewModel
    /// {
    ///     [Bind] public bool _isHighReverbMix;
    /// }
    /// </code>
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField] private AudioSource _audioSource;
    ///     [SerializeField] private float _highReverbMix;
    ///     [SerializeField] private float _lowReverbMix;
    ///    
    ///     private AudioSourceReverbZoneMixSwitcherBinder IsHighReverbMix => new(
    ///         _audioSource, _highReverbMix, _lowReverbMix);
    /// }
    ///    
    /// [ViewModel]
    /// public partial class ExampleViewModel
    /// {
    ///     [Bind] public bool _isHighReverbMix;
    /// }
    /// </code>
    /// </example>
    [Serializable]
    public sealed class AudioSourceReverbZoneMixSwitcherBinder : SwitcherBinder<AudioSource, float, Converter>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="AudioSourceReverbZoneMixSwitcherBinder"/> targeting the specified <see cref="AudioSource"/>
        /// with no converter.
        /// </summary>
        /// <param name="target">The <see cref="AudioSource"/> whose <see cref="AudioSource.reverbZoneMix"/> property is switched.</param>
        /// <param name="trueValue">The reverbZoneMix assigned when the bound value is <see langword="true"/>.</param>
        /// <param name="falseValue">The reverbZoneMix assigned when the bound value is <see langword="false"/>.</param>
        /// <param name="mode">The binding mode to use.</param>
        public AudioSourceReverbZoneMixSwitcherBinder(
            AudioSource target,
            float trueValue,
            float falseValue,
            BindMode mode)
            : this(target, trueValue, falseValue, converter: null, mode) { }

        /// <summary>
        /// Initializes a new instance of <see cref="AudioSourceReverbZoneMixSwitcherBinder"/> targeting the specified <see cref="AudioSource"/>.
        /// </summary>
        /// <param name="target">The <see cref="AudioSource"/> whose <see cref="AudioSource.reverbZoneMix"/> property is switched.</param>
        /// <param name="trueValue">The reverbZoneMix assigned when the bound value is <see langword="true"/>.</param>
        /// <param name="falseValue">The reverbZoneMix assigned when the bound value is <see langword="false"/>.</param>
        /// <param name="converter">The converter used to transform the bound float value, or <see langword="null"/> to use the default.</param>
        /// <param name="mode">The binding mode to use.</param>
        public AudioSourceReverbZoneMixSwitcherBinder(
            AudioSource target,
            float trueValue,
            float falseValue,
            Converter? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, converter, mode) { }

        /// <summary>
        /// Called when applying the selected value to the <see cref="AudioSource.reverbZoneMix"/> property.
        /// Clamps the value to the valid range of 0 to 1.1.
        /// </summary>
        protected override void SetValue(float value) =>
            Target.reverbZoneMix = Mathf.Clamp(value, min: 0, max: 1.1f);
    }
}