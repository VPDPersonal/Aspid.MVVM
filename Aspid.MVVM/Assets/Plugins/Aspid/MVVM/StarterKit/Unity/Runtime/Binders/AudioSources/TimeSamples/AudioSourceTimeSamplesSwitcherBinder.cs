#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherIntBinder{AudioSource}"/> that switches the <see cref="AudioSource.timeSamples"/>
    /// property between two <see cref="int"/> values based on the bound boolean ViewModel value.
    /// </summary>
    /// <include file="XmlExampleDoc-AudioSource-TimeSamples-1.1.0.xml" path="doc//member[@name='AudioSourceTimeSamplesSwitcherBinder']/*" />
    [Serializable]
    public sealed class AudioSourceTimeSamplesSwitcherBinder : SwitcherIntBinder<AudioSource>
    {
        /// <inheritdoc/>
        public AudioSourceTimeSamplesSwitcherBinder(
            AudioSource target,
            int trueValue,
            int falseValue,
            IConverter<int, int>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, converter, mode) { }

        /// <inheritdoc/>
        protected override void SetValue(int value) =>
            Target.timeSamples = value;
    }
}