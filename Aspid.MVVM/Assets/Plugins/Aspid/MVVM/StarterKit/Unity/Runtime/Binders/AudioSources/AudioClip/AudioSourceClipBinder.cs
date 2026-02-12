#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime, BindMode.OneWayToSource)]
    public class AudioSourceClipBinder : TargetBinder<AudioSource>, IBinder<AudioClip?>, IReverseBinder<AudioClip>
    {
        public event Action<AudioClip?>? ValueChanged;
        
        public AudioSourceClipBinder(AudioSource target, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
        }

        public void SetValue(AudioClip? value) =>
            Target.clip = value;
        
        protected override void OnBound()
        {
            if (Mode is BindMode.OneWayToSource)
                ValueChanged?.Invoke(Target.clip);
        }
    }
}