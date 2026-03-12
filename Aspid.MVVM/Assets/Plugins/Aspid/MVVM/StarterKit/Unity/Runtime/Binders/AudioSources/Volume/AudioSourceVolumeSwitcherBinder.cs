#nullable enable
using System;
using UnityEngine;

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
    /// <include file="XmlExampleDoc-AudioSource-Volume-1.1.0.xml" path="doc//member[@name='AudioSourceVolumeSwitcherBinder']/*" />
    [Serializable]
    public sealed class AudioSourceVolumeSwitcherBinder : SwitcherFloatBinder<AudioSource>
    {
        /// <inheritdoc />
        public AudioSourceVolumeSwitcherBinder(
            AudioSource target,
            float trueValue,
            float falseValue,
            IConverter<float, float>? converter = null,
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