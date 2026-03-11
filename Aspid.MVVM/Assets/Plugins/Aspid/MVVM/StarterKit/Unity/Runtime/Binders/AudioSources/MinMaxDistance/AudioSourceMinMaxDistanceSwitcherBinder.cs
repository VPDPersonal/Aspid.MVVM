#nullable enable
using System;
using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Vector2, UnityEngine.Vector2>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterVector2;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherBinder{AudioSource, Vector2, IConverter{Vector2, Vector2}}"/> that switches the
    /// min/max distance of an <see cref="AudioSource"/> between two <see cref="Vector2"/> values
    /// based on the bound boolean ViewModel value.
    /// </summary>
    /// <example>
    /// Switch the AudioSource min/max distance between two values based on a boolean ViewModel property.
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField]
    ///     private AudioSourceMinMaxDistanceSwitcherBinder _isNearRange;
    /// }
    ///    
    /// [ViewModel]
    /// public partial class ExampleViewModel
    /// {
    ///     [Bind] public bool _isNearRange;
    /// }
    /// </code>
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField] private AudioSource _audioSource;
    ///     [SerializeField] private Vector2 _nearRange;
    ///     [SerializeField] private Vector2 _farRange;
    ///    
    ///     private AudioSourceMinMaxDistanceSwitcherBinder IsNearRange => new(
    ///         _audioSource, _nearRange, _farRange);
    /// }
    ///    
    /// [ViewModel]
    /// public partial class ExampleViewModel
    /// {
    ///     [Bind] public bool _isNearRange;
    /// }
    /// </code>
    /// </example>
    [Serializable]
    public sealed class AudioSourceMinMaxDistanceSwitcherBinder : SwitcherBinder<AudioSource, Vector2, Converter>
    {
        [SerializeField] private AudioSourceDistanceMode _distanceMode = AudioSourceDistanceMode.Range;

        /// <summary>
        /// Initializes a new instance of <see cref="AudioSourceMinMaxDistanceSwitcherBinder"/> targeting the specified <see cref="AudioSource"/>
        /// with the default distance mode (<see cref="AudioSourceDistanceMode.Range"/>) and no converter.
        /// </summary>
        /// <param name="target">The <see cref="AudioSource"/> whose min/max distance is switched.</param>
        /// <param name="trueValue">The min/max distance assigned when the bound value is <see langword="true"/>.</param>
        /// <param name="falseValue">The min/max distance assigned when the bound value is <see langword="false"/>.</param>
        /// <param name="mode">The binding mode to use.</param>
        public AudioSourceMinMaxDistanceSwitcherBinder(
            AudioSource target,
            Vector2 trueValue,
            Vector2 falseValue,
            BindMode mode)
            : this(target, trueValue, falseValue, AudioSourceDistanceMode.Range, converter: null, mode) { }

        /// <summary>
        /// Initializes a new instance of <see cref="AudioSourceMinMaxDistanceSwitcherBinder"/> targeting the specified <see cref="AudioSource"/>
        /// with the specified distance mode and no converter.
        /// </summary>
        /// <param name="target">The <see cref="AudioSource"/> whose min/max distance is switched.</param>
        /// <param name="trueValue">The min/max distance assigned when the bound value is <see langword="true"/>.</param>
        /// <param name="falseValue">The min/max distance assigned when the bound value is <see langword="false"/>.</param>
        /// <param name="distanceMode">The <see cref="AudioSourceDistanceMode"/> that determines which distance component is updated.</param>
        /// <param name="mode">The binding mode to use.</param>
        public AudioSourceMinMaxDistanceSwitcherBinder(
            AudioSource target,
            Vector2 trueValue,
            Vector2 falseValue,
            AudioSourceDistanceMode distanceMode,
            BindMode mode)
            : this(target, trueValue, falseValue, distanceMode, converter: null, mode) { }

        /// <summary>
        /// Initializes a new instance of <see cref="AudioSourceMinMaxDistanceSwitcherBinder"/> targeting the specified <see cref="AudioSource"/>
        /// with the default distance mode (<see cref="AudioSourceDistanceMode.Range"/>) and the specified converter.
        /// </summary>
        /// <param name="target">The <see cref="AudioSource"/> whose min/max distance is switched.</param>
        /// <param name="trueValue">The min/max distance assigned when the bound value is <see langword="true"/>.</param>
        /// <param name="falseValue">The min/max distance assigned when the bound value is <see langword="false"/>.</param>
        /// <param name="converter">The converter used to transform the bound <see cref="Vector2"/> value, or <see langword="null"/> to use none.</param>
        /// <param name="mode">The binding mode to use.</param>
        public AudioSourceMinMaxDistanceSwitcherBinder(
            AudioSource target,
            Vector2 trueValue,
            Vector2 falseValue,
            Converter? converter,
            BindMode mode = BindMode.OneWay)
            : this(target, trueValue, falseValue, AudioSourceDistanceMode.Range, converter, mode) { }

        /// <summary>
        /// Initializes a new instance of <see cref="AudioSourceMinMaxDistanceSwitcherBinder"/> targeting the specified <see cref="AudioSource"/>.
        /// </summary>
        /// <param name="target">The <see cref="AudioSource"/> whose min/max distance is switched.</param>
        /// <param name="trueValue">The min/max distance assigned when the bound value is <see langword="true"/>.</param>
        /// <param name="falseValue">The min/max distance assigned when the bound value is <see langword="false"/>.</param>
        /// <param name="distanceMode">The <see cref="AudioSourceDistanceMode"/> that determines which distance component is updated.</param>
        /// <param name="converter">The converter used to transform the bound <see cref="Vector2"/> value, or <see langword="null"/> to use none.</param>
        /// <param name="mode">The binding mode to use.</param>
        public AudioSourceMinMaxDistanceSwitcherBinder(
            AudioSource target,
            Vector2 trueValue,
            Vector2 falseValue,
            AudioSourceDistanceMode distanceMode = AudioSourceDistanceMode.Range,
            Converter? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, converter, mode)
        {
            _distanceMode = distanceMode;
        }

        /// <summary>
        /// Called when applying the selected <see cref="Vector2"/> to the <see cref="AudioSource"/> min/max distance.
        /// Dispatches to <see cref="AudioSource.minDistance"/>, <see cref="AudioSource.maxDistance"/>, or both
        /// according to the configured <see cref="AudioSourceDistanceMode"/>.
        /// </summary>
        protected override void SetValue(Vector2 value) =>
            Target.SetMinMaxDistance(value, _distanceMode);
    }
}