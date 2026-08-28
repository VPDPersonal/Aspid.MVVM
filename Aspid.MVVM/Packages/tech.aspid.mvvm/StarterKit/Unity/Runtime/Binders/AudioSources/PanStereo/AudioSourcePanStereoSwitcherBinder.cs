#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherBinder{TTarget,T}">SwitcherBinder&lt;AudioSource, float&gt;</see> that switches the <see cref="AudioSource.panStereo"/>
    /// property between two <see cref="float"/> values based on the bound boolean ViewModel value.
    /// </summary>
    /// <remarks>
    /// The bound value is clamped to [−1, 1] before being applied to <see cref="AudioSource.panStereo"/>.
    /// </remarks>
    /// <include file="XmlExampleDoc-AudioSource-PanStereo-1.1.0.xml" path="doc//member[@name='AudioSourcePanStereoSwitcherBinder']/*" />
    [Serializable]
    public sealed class AudioSourcePanStereoSwitcherBinder : SwitcherBinder<AudioSource, float>
    {
        /// <inheritdoc/>
        public AudioSourcePanStereoSwitcherBinder(
            AudioSource target,
            float trueValue,
            float falseValue,
            IConverter<float, float>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, converter, mode) { }

        /// <inheritdoc/>
        protected override void SetValue(float value) =>
            Target.panStereo = value;

        /// <summary>
        /// Called when converting the bound value before applying it to the <see cref="AudioSource.panStereo"/> property.
        /// Clamps the converted value to the valid range of −1 to 1.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        protected override float GetConvertedValue(float value) =>
            this.SafeClamp(base.GetConvertedValue(value), -1, 1, Target);
    }
}