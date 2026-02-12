#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
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