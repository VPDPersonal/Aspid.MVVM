#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Binder that switches the <see cref="AudioSource.clip"/> between two <see cref="AudioClip"/>
    /// values based on a bound boolean ViewModel property.
    /// </summary>
    [Serializable]
    public sealed class AudioSourceClipSwitcherBinder : SwitcherBinder<AudioSource, AudioClip?>
    {
        public AudioSourceClipSwitcherBinder(
            AudioSource target,
            AudioClip? trueValue,
            AudioClip? falseValue,
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, mode) { }
        
        protected override void SetValue(AudioClip? value) =>
            Target.clip = value;
    }
}