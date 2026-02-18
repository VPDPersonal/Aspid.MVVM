#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public class AudioSourceBypassReverbZonesBinder : TargetBoolBinder<AudioSource>
    {
        protected sealed override bool Property
        {
            get => Target.bypassReverbZones;
            set => Target.bypassReverbZones = value;
        }
        
        public AudioSourceBypassReverbZonesBinder(AudioSource target, BindMode mode)
            : this(target, isInvert: false, mode) { }
        
        public AudioSourceBypassReverbZonesBinder(AudioSource target, bool isInvert = false, BindMode mode = BindMode.OneWay)
            : base(target, isInvert, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}