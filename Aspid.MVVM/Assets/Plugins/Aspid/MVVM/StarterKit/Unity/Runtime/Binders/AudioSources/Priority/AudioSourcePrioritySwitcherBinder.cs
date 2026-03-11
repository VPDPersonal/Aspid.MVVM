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
    /// <see cref="SwitcherBinder{AudioSource, int, IConverter{int, int}}"/> that switches the <see cref="AudioSource.priority"/>
    /// property between two <see cref="int"/> values based on the bound boolean ViewModel value.
    /// </summary>
    /// <remarks>
    /// The bound value is clamped to [0, 256] before being applied to <see cref="AudioSource.priority"/>.
    /// </remarks>
    /// <example>
    /// Switch the AudioSource priority between two values based on a boolean ViewModel property.
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField]
    ///     private AudioSourcePrioritySwitcherBinder _isHighPriority;
    /// }
    ///    
    /// [ViewModel]
    /// public partial class ExampleViewModel
    /// {
    ///     [Bind] public bool _isHighPriority;
    /// }
    /// </code>
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField] private AudioSource _audioSource;
    ///     [SerializeField] private int _highPriority;
    ///     [SerializeField] private int _lowPriority;
    ///    
    ///     private AudioSourcePrioritySwitcherBinder IsHighPriority => new(
    ///         _audioSource, _highPriority, _lowPriority);
    /// }
    ///    
    /// [ViewModel]
    /// public partial class ExampleViewModel
    /// {
    ///     [Bind] public bool _isHighPriority;
    /// }
    /// </code>
    /// </example>
    [Serializable]
    public sealed class AudioSourcePrioritySwitcherBinder : SwitcherBinder<AudioSource, int, Converter>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="AudioSourcePrioritySwitcherBinder"/> targeting the specified <see cref="AudioSource"/>
        /// with no converter.
        /// </summary>
        /// <param name="target">The <see cref="AudioSource"/> whose <see cref="AudioSource.priority"/> property is switched.</param>
        /// <param name="trueValue">The priority assigned when the bound value is <see langword="true"/>.</param>
        /// <param name="falseValue">The priority assigned when the bound value is <see langword="false"/>.</param>
        /// <param name="mode">The binding mode to use.</param>
        public AudioSourcePrioritySwitcherBinder(
            AudioSource target,
            int trueValue,
            int falseValue,
            BindMode mode)
            : this(target, trueValue, falseValue, converter: null, mode) { }

        /// <summary>
        /// Initializes a new instance of <see cref="AudioSourcePrioritySwitcherBinder"/> targeting the specified <see cref="AudioSource"/>.
        /// </summary>
        /// <param name="target">The <see cref="AudioSource"/> whose <see cref="AudioSource.priority"/> property is switched.</param>
        /// <param name="trueValue">The priority assigned when the bound value is <see langword="true"/>.</param>
        /// <param name="falseValue">The priority assigned when the bound value is <see langword="false"/>.</param>
        /// <param name="converter">The converter used to transform the bound integer value, or <see langword="null"/> to use the default.</param>
        /// <param name="mode">The binding mode to use.</param>
        public AudioSourcePrioritySwitcherBinder(
            AudioSource target,
            int trueValue,
            int falseValue,
            Converter? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, converter, mode) { }

        /// <summary>
        /// Called when applying the selected value to the <see cref="AudioSource.priority"/> property.
        /// Clamps the value to the valid range of 0 to 256.
        /// </summary>
        protected override void SetValue(int value) =>
            Target.priority = Mathf.Clamp(value, min: 0, max: 256);
    }
}