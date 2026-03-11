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
    /// <see cref="TargetFloatBinder{AudioSource}"/> that sets the <see cref="AudioSource.pitch"/> property.
    /// </summary>
    /// <remarks>
    /// The bound value is clamped to [−3, 3] before being applied to <see cref="AudioSource.pitch"/>.
    /// </remarks>
    /// <example>
    /// Set the AudioSource pitch based on a float ViewModel value.
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField]
    ///     private AudioSourcePitchBinder _pitch;
    /// }
    ///    
    /// [ViewModel]
    /// public partial class ExampleViewModel
    /// {
    ///     [Bind] public float _pitch;
    /// }
    /// </code>
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField] private AudioSource _audioSource;
    ///    
    ///     private AudioSourcePitchBinder Pitch =>
    ///         new(_audioSource);
    /// }
    ///    
    /// [ViewModel]
    /// public partial class ExampleViewModel
    /// {
    ///     [Bind] public float _pitch;
    /// }
    /// </code>
    /// </example>
    [Serializable]
    public class AudioSourcePitchBinder : TargetFloatBinder<AudioSource>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => Target.pitch;
            set => Target.pitch = value;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="AudioSourcePitchBinder"/> targeting the specified <see cref="AudioSource"/>
        /// with no converter.
        /// </summary>
        /// <param name="target">The <see cref="AudioSource"/> whose <see cref="AudioSource.pitch"/> property is bound.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public AudioSourcePitchBinder(AudioSource target, BindMode mode)
            : this(target, converter: null, mode) { }

        /// <summary>
        /// Initializes a new instance of <see cref="AudioSourcePitchBinder"/> targeting the specified <see cref="AudioSource"/>.
        /// </summary>
        /// <param name="target">The <see cref="AudioSource"/> whose <see cref="AudioSource.pitch"/> property is bound.</param>
        /// <param name="converter">The converter used to transform the bound float value, or <see langword="null"/> to use the default.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public AudioSourcePitchBinder(AudioSource target, Converter? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }

        /// <summary>
        /// Called when converting the bound value before applying it to the <see cref="AudioSource.pitch"/> property.
        /// Clamps the converted value to the valid range of −3 to 3.
        /// </summary>
        /// <remarks>
        /// When overriding this method, always call <c>base.GetConvertedValue(value)</c> to preserve
        /// the clamping behavior.
        /// </remarks>
        protected override float GetConvertedValue(float value) =>
            Mathf.Clamp(base.GetConvertedValue(value), min: -3, max: 3);
    }
}