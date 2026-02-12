#nullable enable
using System;
using UnityEngine;
using UnityEngine.Audio;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime, BindMode.OneWayToSource)]
    public class AudioSourceOutputAudioMixerGroupBinder : TargetBinder<AudioSource>, IBinder<AudioMixerGroup?>, IReverseBinder<AudioMixerGroup?>
    {
        public event Action<AudioMixerGroup?>? ValueChanged;
        
        public AudioSourceOutputAudioMixerGroupBinder(AudioSource target, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
        }

        public void SetValue(AudioMixerGroup? value) =>
            Target.outputAudioMixerGroup = value;
        
        protected override void OnBound()
        {
            if (Mode is BindMode.OneWayToSource)
                ValueChanged?.Invoke(Target.outputAudioMixerGroup);
        }
    }
}