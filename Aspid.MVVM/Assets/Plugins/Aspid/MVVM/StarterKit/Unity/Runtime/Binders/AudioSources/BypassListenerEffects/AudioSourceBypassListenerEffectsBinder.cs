#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBoolBinder{AudioSource}"/> that sets the <see cref="AudioSource.bypassListenerEffects"/> property.
    /// </summary>
    /// <include file="XmlExampleDoc-AudioSource-BypassListenerEffects-1.1.0.xml" path="doc//member[@name='AudioSourceBypassListenerEffectsBinder']/*"></include>
    [Serializable]
    public class AudioSourceBypassListenerEffectsBinder : TargetBoolBinder<AudioSource>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => Target.bypassListenerEffects;
            set => Target.bypassListenerEffects = value;
        }

        /// <inheritdoc/>
        public AudioSourceBypassListenerEffectsBinder(AudioSource target, bool isInvert = false, BindMode mode = BindMode.OneWay)
            : base(target, isInvert, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}