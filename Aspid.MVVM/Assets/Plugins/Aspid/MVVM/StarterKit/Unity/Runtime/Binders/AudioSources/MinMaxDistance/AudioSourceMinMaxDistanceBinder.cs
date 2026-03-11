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
    /// <see cref="TargetBinder{AudioSource, Vector2}"/> that sets the min/max distance of an <see cref="AudioSource"/>
    /// as a <see cref="Vector2"/>.
    /// </summary>
    /// <remarks>
    /// Also implements <see cref="INumberBinder"/>, allowing scalar numeric values to set both
    /// <see cref="AudioSource.minDistance"/> and <see cref="AudioSource.maxDistance"/> simultaneously.
    /// </remarks>
    /// <example>
    /// Set the AudioSource min/max distance based on a Vector2 ViewModel value.
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField]
    ///     private AudioSourceMinMaxDistanceBinder _minMaxDistance;
    /// }
    ///    
    /// [ViewModel]
    /// public partial class ExampleViewModel
    /// {
    ///     [Bind] public Vector2 _minMaxDistance;
    /// }
    /// </code>
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField] private AudioSource _audioSource;
    ///    
    ///     private AudioSourceMinMaxDistanceBinder MinMaxDistance =>
    ///         new(_audioSource);
    /// }
    ///    
    /// [ViewModel]
    /// public partial class ExampleViewModel
    /// {
    ///     [Bind] public Vector2 _minMaxDistance;
    /// }
    /// </code>
    /// </example>
    [Serializable]
    public class AudioSourceMinMaxDistanceBinder : TargetBinder<AudioSource, Vector2>, INumberBinder
    {
        [SerializeField] private AudioSourceDistanceMode _distanceMode = AudioSourceDistanceMode.Range;

        [SerializeReferenceDropdown]
        [SerializeReference] private Converter? _converter;

        /// <inheritdoc/>
        protected sealed override Vector2 Property
        {
            get => new(Target.minDistance, Target.maxDistance);
            set => Target.SetMinMaxDistance(value, _distanceMode);
        }

        /// <summary>
        /// Initializes a new instance of <see cref="AudioSourceMinMaxDistanceBinder"/> targeting the specified <see cref="AudioSource"/>
        /// with the default distance mode (<see cref="AudioSourceDistanceMode.Range"/>) and no converter.
        /// </summary>
        /// <param name="target">The <see cref="AudioSource"/> whose min/max distance is bound.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public AudioSourceMinMaxDistanceBinder(
            AudioSource target,
            BindMode mode)
            : this(target, AudioSourceDistanceMode.Range, converter: null, mode) { }

        /// <summary>
        /// Initializes a new instance of <see cref="AudioSourceMinMaxDistanceBinder"/> targeting the specified <see cref="AudioSource"/>
        /// with the specified distance mode and no converter.
        /// </summary>
        /// <param name="target">The <see cref="AudioSource"/> whose min/max distance is bound.</param>
        /// <param name="distanceMode">The <see cref="AudioSourceDistanceMode"/> that determines which distance component is updated.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public AudioSourceMinMaxDistanceBinder(
            AudioSource target,
            AudioSourceDistanceMode distanceMode,
            BindMode mode)
            : this(target, distanceMode, converter: null, mode) { }

        /// <summary>
        /// Initializes a new instance of <see cref="AudioSourceMinMaxDistanceBinder"/> targeting the specified <see cref="AudioSource"/>
        /// with the default distance mode (<see cref="AudioSourceDistanceMode.Range"/>) and the specified converter.
        /// </summary>
        /// <param name="target">The <see cref="AudioSource"/> whose min/max distance is bound.</param>
        /// <param name="converter">The converter used to transform the bound <see cref="Vector2"/> value, or <see langword="null"/> to use none.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public AudioSourceMinMaxDistanceBinder(
            AudioSource target,
            Converter? converter,
            BindMode mode = BindMode.OneWay)
            : this(target, AudioSourceDistanceMode.Range, converter, mode) { }

        /// <summary>
        /// Initializes a new instance of <see cref="AudioSourceMinMaxDistanceBinder"/> targeting the specified <see cref="AudioSource"/>.
        /// </summary>
        /// <param name="target">The <see cref="AudioSource"/> whose min/max distance is bound.</param>
        /// <param name="distanceMode">The <see cref="AudioSourceDistanceMode"/> that determines which distance component is updated.</param>
        /// <param name="converter">The converter used to transform the bound <see cref="Vector2"/> value, or <see langword="null"/> to use none.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public AudioSourceMinMaxDistanceBinder(
            AudioSource target,
            AudioSourceDistanceMode distanceMode = AudioSourceDistanceMode.Range,
            Converter? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
            _distanceMode = distanceMode;
            _converter = converter;
        }

        /// <summary>
        /// Sets both <see cref="AudioSource.minDistance"/> and <see cref="AudioSource.maxDistance"/>
        /// to <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The value to assign to both distance properties.</param>
        public void SetValue(float value) =>
            SetValue(new Vector2(value, value));

        /// <summary>
        /// Converts <paramref name="value"/> to <see cref="float"/> and calls <see cref="SetValue(float)"/>.
        /// </summary>
        /// <param name="value">The value to convert and apply.</param>
        public void SetValue(int value) =>
            SetValue((float)value);

        /// <summary>
        /// Converts <paramref name="value"/> to <see cref="float"/> and calls <see cref="SetValue(float)"/>.
        /// </summary>
        /// <param name="value">The value to convert and apply.</param>
        public void SetValue(long value) =>
            SetValue((float)value);

        /// <summary>
        /// Converts <paramref name="value"/> to <see cref="float"/> and calls <see cref="SetValue(float)"/>.
        /// </summary>
        /// <param name="value">The value to convert and apply.</param>
        public void SetValue(double value) =>
            SetValue((float)value);

        /// <summary>
        /// Called when converting the bound value before applying it to the min/max distance.
        /// Applies the stored converter if one was provided; otherwise returns the value unchanged.
        /// </summary>
        protected override Vector2 GetConvertedValue(Vector2 value) =>
            _converter?.Convert(value) ?? value;
    }
}