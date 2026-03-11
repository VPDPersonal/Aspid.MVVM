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
    /// <see cref="SwitcherBinder{AudioSource, float, IConverter{float, float}}"/> that switches the <see cref="AudioSource.volume"/>
    /// property between two <see cref="float"/> values based on the bound boolean ViewModel value.
    /// </summary>
    /// <remarks>
    /// The bound value is clamped to [0, 1] before being applied to <see cref="AudioSource.volume"/>.
    /// </remarks>
    /// <example>
    /// Switch the AudioSource volume between two values based on a boolean ViewModel property.
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField]
    ///     private AudioSourceVolumeSwitcherBinder _isHighVolume;
    /// }
    ///    
    /// [ViewModel]
    /// public partial class ExampleViewModel
    /// {
    ///     [Bind] public bool _isHighVolume;
    /// }
    /// </code>
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField] private AudioSource _audioSource;
    ///     [SerializeField] private float _highVolume;
    ///     [SerializeField] private float _lowVolume;
    ///    
    ///     private AudioSourceVolumeSwitcherBinder IsHighVolume => new(
    ///         _audioSource, _highVolume, _lowVolume);
    /// }
    ///    
    /// [ViewModel]
    /// public partial class ExampleViewModel
    /// {
    ///     [Bind] public bool _isHighVolume;
    /// }
    /// </code>
    /// </example>
    [Serializable]
    public sealed class AudioSourceVolumeSwitcherBinder : SwitcherBinder<AudioSource, float, Converter>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="AudioSourceVolumeSwitcherBinder"/> targeting the specified <see cref="AudioSource"/>
        /// with no converter.
        /// </summary>
        /// <param name="target">The <see cref="AudioSource"/> whose <see cref="AudioSource.volume"/> property is switched.</param>
        /// <param name="trueValue">The volume assigned when the bound value is <see langword="true"/>.</param>
        /// <param name="falseValue">The volume assigned when the bound value is <see langword="false"/>.</param>
        /// <param name="mode">The binding mode to use.</param>
        public AudioSourceVolumeSwitcherBinder(
            AudioSource target,
            float trueValue,
            float falseValue,
            BindMode mode)
            : this(target, trueValue, falseValue, converter: null, mode) { }

        /// <summary>
        /// Initializes a new instance of <see cref="AudioSourceVolumeSwitcherBinder"/> targeting the specified <see cref="AudioSource"/>.
        /// </summary>
        /// <param name="target">The <see cref="AudioSource"/> whose <see cref="AudioSource.volume"/> property is switched.</param>
        /// <param name="trueValue">The volume assigned when the bound value is <see langword="true"/>.</param>
        /// <param name="falseValue">The volume assigned when the bound value is <see langword="false"/>.</param>
        /// <param name="converter">The converter used to transform the bound float value, or <see langword="null"/> to use the default.</param>
        /// <param name="mode">The binding mode to use.</param>
        public AudioSourceVolumeSwitcherBinder(
            AudioSource target,
            float trueValue,
            float falseValue,
            Converter? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, converter, mode) { }

        /// <summary>
        /// Called when applying the selected value to the <see cref="AudioSource.volume"/> property.
        /// Clamps the value to the valid range of 0 to 1.
        /// </summary>
        protected override void SetValue(float value) =>
            Target.volume = Mathf.Clamp(value, min: 0, max: 1);
    }
}