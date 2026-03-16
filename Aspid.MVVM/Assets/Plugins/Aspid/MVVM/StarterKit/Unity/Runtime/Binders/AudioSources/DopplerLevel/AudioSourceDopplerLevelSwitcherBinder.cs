#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherFloatBinder{AudioSource}"/> that switches the <see cref="AudioSource.dopplerLevel"/>
    /// property between two <see cref="float"/> values based on the bound boolean ViewModel value.
    /// </summary>
    /// <include file="XmlExampleDoc-AudioSource-DopplerLevel-1.1.0.xml" path="doc//member[@name='AudioSourceDopplerLevelSwitcherBinder']/*" />
    [Serializable]
    public sealed class AudioSourceDopplerLevelSwitcherBinder : SwitcherFloatBinder<AudioSource>
    {
        /// <inheritdoc/>
        public AudioSourceDopplerLevelSwitcherBinder(
            AudioSource target,
            float trueValue,
            float falseValue,
            IConverter<float, float>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, converter, mode) { }

        /// <inheritdoc/>
        protected override void SetValue(float value) =>
            Target.dopplerLevel = value;
    }
}