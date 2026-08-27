#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherBinderWithConverter{T1, T2}">SwitcherBinderWithConverter&lt;AudioSource, Vector2&gt;</see> that switches the
    /// min/max distance of an <see cref="AudioSource"/> between two <see cref="Vector2"/> values
    /// based on the bound boolean ViewModel value.
    /// </summary>
    /// <include file="XmlExampleDoc-AudioSource-MinMaxDistance-1.1.0.xml" path="doc//member[@name='AudioSourceMinMaxDistanceSwitcherBinder']/*" />
    [Serializable]
    public sealed class AudioSourceMinMaxDistanceSwitcherBinder : SwitcherBinderWithConverter<AudioSource, Vector2>
    {
        [Tooltip("Which distance component the bound value updates.")]
        [SerializeField] private AudioSourceDistanceMode _distanceMode = AudioSourceDistanceMode.Range;
        
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
            IConverter<Vector2, Vector2>? converter = null,
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
        /// <param name="value">The value received from the ViewModel.</param>
        protected override void SetValue(Vector2 value) =>
            Target.SetMinMaxDistance(value, _distanceMode);
    }
}