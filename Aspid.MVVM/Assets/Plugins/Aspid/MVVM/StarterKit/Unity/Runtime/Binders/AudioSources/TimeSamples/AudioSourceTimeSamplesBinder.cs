#nullable enable
using System;
using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<int, int>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterInt;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetIntBinder{AudioSource}"/> that sets the <see cref="AudioSource.timeSamples"/> property.
    /// </summary>
    /// <example>
    /// Set the AudioSource timeSamples based on an integer ViewModel value.
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField]
    ///     private AudioSourceTimeSamplesBinder _timeSamples;
    /// }
    ///    
    /// [ViewModel]
    /// public partial class ExampleViewModel
    /// {
    ///     [Bind] public int _timeSamples;
    /// }
    /// </code>
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField] private AudioSource _audioSource;
    ///    
    ///     private AudioSourceTimeSamplesBinder TimeSamples =>
    ///         new(_audioSource);
    /// }
    ///    
    /// [ViewModel]
    /// public partial class ExampleViewModel
    /// {
    ///     [Bind] public int _timeSamples;
    /// }
    /// </code>
    /// </example>
    [Serializable]
    public class AudioSourceTimeSamplesBinder : TargetIntBinder<AudioSource>
    {
        /// <inheritdoc/>
        protected sealed override int Property
        {
            get => Target.timeSamples;
            set => Target.timeSamples = value;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="AudioSourceTimeSamplesBinder"/> targeting the specified <see cref="AudioSource"/>
        /// with no converter.
        /// </summary>
        /// <param name="target">The <see cref="AudioSource"/> whose <see cref="AudioSource.timeSamples"/> property is bound.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public AudioSourceTimeSamplesBinder(AudioSource target, BindMode mode)
            : this(target, converter: null, mode) { }

        /// <summary>
        /// Initializes a new instance of <see cref="AudioSourceTimeSamplesBinder"/> targeting the specified <see cref="AudioSource"/>.
        /// </summary>
        /// <param name="target">The <see cref="AudioSource"/> whose <see cref="AudioSource.timeSamples"/> property is bound.</param>
        /// <param name="converter">The converter used to transform the bound integer value, or <see langword="null"/> to use the default.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public AudioSourceTimeSamplesBinder(AudioSource target, Converter? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}