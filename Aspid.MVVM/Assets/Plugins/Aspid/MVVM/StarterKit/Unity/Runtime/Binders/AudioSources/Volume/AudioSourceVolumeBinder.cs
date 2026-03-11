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
    /// <see cref="TargetFloatBinder{AudioSource}"/> that sets the <see cref="AudioSource.volume"/> property.
    /// </summary>
    /// <remarks>
    /// The bound value is clamped to [0, 1] before being applied to <see cref="AudioSource.volume"/>.
    /// </remarks>
    /// <example>
    /// Set the AudioSource volume based on a float ViewModel value.
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField]
    ///     private AudioSourceVolumeBinder _volume;
    /// }
    ///    
    /// [ViewModel]
    /// public partial class ExampleViewModel
    /// {
    ///     [Bind] public float _volume;
    /// }
    /// </code>
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField] private AudioSource _audioSource;
    ///    
    ///     private AudioSourceVolumeBinder Volume =>
    ///         new(_audioSource);
    /// }
    ///    
    /// [ViewModel]
    /// public partial class ExampleViewModel
    /// {
    ///     [Bind] public float _volume;
    /// }
    /// </code>
    /// </example>
    [Serializable]
    public class AudioSourceVolumeBinder : TargetFloatBinder<AudioSource>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => Target.volume;
            set => Target.volume = value;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="AudioSourceVolumeBinder"/> targeting the specified <see cref="AudioSource"/>
        /// with no converter.
        /// </summary>
        /// <param name="target">The <see cref="AudioSource"/> whose <see cref="AudioSource.volume"/> property is bound.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public AudioSourceVolumeBinder(AudioSource target, BindMode mode)
            : this(target, converter: null, mode) { }

        /// <summary>
        /// Initializes a new instance of <see cref="AudioSourceVolumeBinder"/> targeting the specified <see cref="AudioSource"/>.
        /// </summary>
        /// <param name="target">The <see cref="AudioSource"/> whose <see cref="AudioSource.volume"/> property is bound.</param>
        /// <param name="converter">The converter used to transform the bound float value, or <see langword="null"/> to use the default.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public AudioSourceVolumeBinder(AudioSource target, Converter? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }

        /// <summary>
        /// Called when converting the bound value before applying it to the <see cref="AudioSource.volume"/> property.
        /// Clamps the converted value to the valid range of 0 to 1.
        /// </summary>
        /// <remarks>
        /// When overriding this method, always call <c>base.GetConvertedValue(value)</c> to preserve
        /// the clamping behavior.
        /// </remarks>
        protected override float GetConvertedValue(float value) =>
            Mathf.Clamp(base.GetConvertedValue(value), min: 0, max: 1);
    }
}