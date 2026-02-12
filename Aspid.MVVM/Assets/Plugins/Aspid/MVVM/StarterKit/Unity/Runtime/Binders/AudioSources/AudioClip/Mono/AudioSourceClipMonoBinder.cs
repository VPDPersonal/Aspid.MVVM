using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(AudioSource))]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – Clip")]
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime, BindMode.OneWayToSource)]
    public partial class AudioSourceClipMonoBinder : ComponentMonoBinder<AudioSource>, IBinder<AudioClip>, IReverseBinder<AudioClip>
    {
        public event Action<AudioClip> ValueChanged;
        
        [BinderLog]
        public void SetValue(AudioClip value) =>
            CachedComponent.clip = value;

        protected override void OnBound()
        {
            if (Mode is BindMode.OneWayToSource)
                ValueChanged?.Invoke(CachedComponent.clip);
        }
    }
}