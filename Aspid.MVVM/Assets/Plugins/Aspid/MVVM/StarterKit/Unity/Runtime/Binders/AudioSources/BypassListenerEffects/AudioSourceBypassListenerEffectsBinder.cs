#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Binder that sets the <see cref="AudioSource.bypassListenerEffects"/> property on an <see cref="AudioSource"/>
    /// when the bound ViewModel value changes. Supports optional value inversion.
    /// </summary>
    [Serializable]
    public class AudioSourceBypassListenerEffectsBinder : TargetBoolBinder<AudioSource>
    {
        protected sealed override bool Property
        {
            get => Target.bypassListenerEffects;
            set => Target.bypassListenerEffects = value;
        }
        
        public AudioSourceBypassListenerEffectsBinder(AudioSource target, BindMode mode)
            : this(target, isInvert: false, mode) { }

        public AudioSourceBypassListenerEffectsBinder(AudioSource target, bool isInvert = false, BindMode mode = BindMode.OneWay)
            : base(target, isInvert, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}