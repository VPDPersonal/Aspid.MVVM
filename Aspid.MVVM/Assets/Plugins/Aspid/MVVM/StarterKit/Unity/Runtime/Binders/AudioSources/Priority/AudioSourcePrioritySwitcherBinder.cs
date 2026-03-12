#nullable enable
using System;
using UnityEngine;

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
    /// <include file="XmlExampleDoc-AudioSource-Priority-1.1.0.xml" path="doc//member[@name='AudioSourcePrioritySwitcherBinder']/*" />
    [Serializable]
    public sealed class AudioSourcePrioritySwitcherBinder : SwitcherIntBinder<AudioSource>
    {
        /// <inheritdoc/>
        public AudioSourcePrioritySwitcherBinder(
            AudioSource target,
            int trueValue,
            int falseValue,
            IConverter<int, int>? converter = null,
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