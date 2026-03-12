#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherBinder{AudioSource, AudioClip}"/> that switches the <see cref="AudioSource.clip"/>
    /// property between two <see cref="AudioClip"/> values based on the bound boolean ViewModel value.
    /// </summary>
    /// <include file="XmlExampleDoc-AudioSource-AudioClip-1.1.0.xml" path="doc//member[@name='AudioSourceClipSwitcherBinder']/*" />
    [Serializable]
    public sealed class AudioSourceClipSwitcherBinder : SwitcherBinder<AudioSource, AudioClip?>
    {
        /// <inheritdoc/>
        public AudioSourceClipSwitcherBinder(
            AudioSource target,
            AudioClip? trueValue,
            AudioClip? falseValue,
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, mode) { }
        
        /// <inheritdoc/>
        protected override void SetValue(AudioClip? value) =>
            Target.clip = value;
    }
}