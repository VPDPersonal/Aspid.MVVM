#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Binder that sets the <see cref="AudioSource.bypassEffects"/> property on an <see cref="AudioSource"/>
    /// when the bound ViewModel value changes. Supports optional value inversion.
    /// </summary>
    [Serializable]
    public class AudioSourceBypassEffectsBinder : TargetBoolBinder<AudioSource>
    {
        protected sealed override bool Property
        {
            get => Target.bypassEffects;
            set => Target.bypassEffects = value;
        }

        public AudioSourceBypassEffectsBinder(AudioSource target, BindMode mode)
            : this(target, isInvert: false, mode) { }

        public AudioSourceBypassEffectsBinder(AudioSource target, bool isInvert = false, BindMode mode = BindMode.OneWay)
            : base(target, isInvert, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}