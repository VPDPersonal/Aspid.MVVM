#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{AudioSource, bool}"/> that sets the <see cref="AudioSource.bypassListenerEffects"/> property.
    /// </summary>
    /// <include file="XmlExampleDoc-AudioSource-BypassListenerEffects-1.1.0.xml" path="doc//member[@name='AudioSourceBypassListenerEffectsBinder']/*"></include>
    [Serializable]
    public class AudioSourceBypassListenerEffectsBinder : TargetBinder<AudioSource, bool>
    {
        /// <inheritdoc/>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/>.</exception>
        public AudioSourceBypassListenerEffectsBinder(AudioSource target, IConverter<bool, bool>? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }

        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => Target.bypassListenerEffects;
            set => Target.bypassListenerEffects = value;
        }
    }
}