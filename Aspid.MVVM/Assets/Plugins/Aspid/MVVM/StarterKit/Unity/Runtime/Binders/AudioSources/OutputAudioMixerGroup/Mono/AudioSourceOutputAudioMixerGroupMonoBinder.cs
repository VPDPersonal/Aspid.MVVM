using System;
using UnityEngine;
using UnityEngine.Audio;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(AudioSource))]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – OutputAudioMixerGroup")]
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime, BindMode.OneWayToSource)]
    public partial class AudioSourceOutputAudioMixerGroupMonoBinder : ComponentMonoBinder<AudioSource>, IBinder<AudioMixerGroup>, IReverseBinder<AudioMixerGroup>
    {
        public event Action<AudioMixerGroup> ValueChanged;
        
        [BinderLog]
        public void SetValue(AudioMixerGroup value) =>
            CachedComponent.outputAudioMixerGroup = value;
        
        protected override void OnBound()
        {
            if (Mode is BindMode.OneWayToSource)
                ValueChanged?.Invoke(CachedComponent.outputAudioMixerGroup);
        }
    }
}