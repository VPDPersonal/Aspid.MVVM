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
    /// Concrete <see cref="TargetBinder{AudioSource, Vector2}"/> that also implements <see cref="INumberBinder"/>,
    /// setting the <see cref="AudioSource.minDistance"/> and <see cref="AudioSource.maxDistance"/> from a <see cref="Vector2"/>.
    /// </summary>
    /// <include file="XmlExampleDoc-AudioSource-MinMaxDistance-1.1.0.xml" path="doc//member[@name='AudioSourceMinMaxDistanceBinder']/*" />
    [Serializable]
    public class AudioSourceMinMaxDistanceBinder : TargetBinder<AudioSource, Vector2>, INumberBinder
    {
        [Tooltip("Determines which distance component (min, max, or both) is updated when the bound value changes.")]
        [SerializeField] private AudioSourceDistanceMode _distanceMode = AudioSourceDistanceMode.Range;

        [SerializeReferenceDropdown]
        [Tooltip("Optional converter applied to the bound Vector2 value before setting the min/max distance.")]
        [SerializeReference] private Converter? _converter;

        /// <inheritdoc/>
        protected sealed override Vector2 Property
        {
            get => new(Target.minDistance, Target.maxDistance);
            set => Target.SetMinMaxDistance(value, _distanceMode);
        }

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
            _converter = converter;
            _distanceMode = distanceMode;
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