#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBoolBinder{AudioSource}"/> that sets the <see cref="AudioSource.bypassEffects"/> property.
    /// </summary>
    /// <include file="XmlExampleDoc-AudioSource-BypassEffects-1.1.0.xml" path="doc//member[@name='AudioSourceBypassEffectsBinder']/*"></include>
    [Serializable]
    public class AudioSourceBypassEffectsBinder : TargetBoolBinder<AudioSource>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => Target.bypassEffects;
            set => Target.bypassEffects = value;
        }

        /// <inheritdoc/>
        public AudioSourceBypassEffectsBinder(AudioSource target, bool isInvert = false, BindMode mode = BindMode.OneWay)
            : base(target, isInvert, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}