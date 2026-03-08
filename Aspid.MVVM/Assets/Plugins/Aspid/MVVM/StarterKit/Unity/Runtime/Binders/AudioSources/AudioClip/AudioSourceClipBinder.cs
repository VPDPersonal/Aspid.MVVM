#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Binder that sets the <see cref="AudioSource.clip"/> property on an <see cref="AudioSource"/>
    /// when the bound ViewModel value changes.
    /// </summary>
    [Serializable]
    public class AudioSourceClipBinder : TargetBinder<AudioSource, AudioClip>
    {
        protected sealed override AudioClip? Property
        {
            get => Target.clip;
            set => Target.clip = value;
        }
        
        public AudioSourceClipBinder(AudioSource target, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}