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
    /// <see cref="SwitcherBinder{AudioSource, int, IConverter{int, int}}"/> that switches the <see cref="AudioSource.timeSamples"/>
    /// property between two <see cref="int"/> values based on the bound boolean ViewModel value.
    /// </summary>
    /// <example>
    /// Switch the AudioSource timeSamples between two values based on a boolean ViewModel property.
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField]
    ///     private AudioSourceTimeSamplesSwitcherBinder _isHighTimeSamples;
    /// }
    ///    
    /// [ViewModel]
    /// public partial class ExampleViewModel
    /// {
    ///     [Bind] public bool _isHighTimeSamples;
    /// }
    /// </code>
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField] private AudioSource _audioSource;
    ///     [SerializeField] private int _highTimeSamples;
    ///     [SerializeField] private int _lowTimeSamples;
    ///    
    ///     private AudioSourceTimeSamplesSwitcherBinder IsHighTimeSamples => new(
    ///         _audioSource, _highTimeSamples, _lowTimeSamples);
    /// }
    ///    
    /// [ViewModel]
    /// public partial class ExampleViewModel
    /// {
    ///     [Bind] public bool _isHighTimeSamples;
    /// }
    /// </code>
    /// </example>
    [Serializable]
    public sealed class AudioSourceTimeSamplesSwitcherBinder : SwitcherBinder<AudioSource, int, Converter>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="AudioSourceTimeSamplesSwitcherBinder"/> targeting the specified <see cref="AudioSource"/>
        /// with no converter.
        /// </summary>
        /// <param name="target">The <see cref="AudioSource"/> whose <see cref="AudioSource.timeSamples"/> property is switched.</param>
        /// <param name="trueValue">The timeSamples assigned when the bound value is <see langword="true"/>.</param>
        /// <param name="falseValue">The timeSamples assigned when the bound value is <see langword="false"/>.</param>
        /// <param name="mode">The binding mode to use.</param>
        public AudioSourceTimeSamplesSwitcherBinder(
            AudioSource target,
            int trueValue,
            int falseValue,
            BindMode mode)
            : this(target, trueValue, falseValue, converter: null, mode) { }

        /// <summary>
        /// Initializes a new instance of <see cref="AudioSourceTimeSamplesSwitcherBinder"/> targeting the specified <see cref="AudioSource"/>.
        /// </summary>
        /// <param name="target">The <see cref="AudioSource"/> whose <see cref="AudioSource.timeSamples"/> property is switched.</param>
        /// <param name="trueValue">The timeSamples assigned when the bound value is <see langword="true"/>.</param>
        /// <param name="falseValue">The timeSamples assigned when the bound value is <see langword="false"/>.</param>
        /// <param name="converter">The converter used to transform the bound integer value, or <see langword="null"/> to use the default.</param>
        /// <param name="mode">The binding mode to use.</param>
        public AudioSourceTimeSamplesSwitcherBinder(
            AudioSource target,
            int trueValue,
            int falseValue,
            Converter? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, converter, mode) { }

        /// <inheritdoc/>
        protected override void SetValue(int value) =>
            Target.timeSamples = value;
    }
}