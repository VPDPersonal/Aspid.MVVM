#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherBinder{AudioSource, float, IConverter{float, float}}"/> that switches the <see cref="AudioSource.time"/>
    /// property between two <see cref="float"/> values based on the bound boolean ViewModel value.
    /// </summary>
    /// <include file="XmlExampleDoc-AudioSource-Time-1.1.0.xml" path="doc//member[@name='AudioSourceTimeSwitcherBinder']/*" />
    [Serializable]
    public sealed class AudioSourceTimeSwitcherBinder : SwitcherFloatBinder<AudioSource>
    {
        /// <inheritdoc/>
        public AudioSourceTimeSwitcherBinder(
            AudioSource target,
            float trueValue,
            float falseValue,
            IConverter<float, float>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, converter, mode) { }

        /// <inheritdoc/>
        protected override void SetValue(float value) =>
            Target.time = value;
    }
}