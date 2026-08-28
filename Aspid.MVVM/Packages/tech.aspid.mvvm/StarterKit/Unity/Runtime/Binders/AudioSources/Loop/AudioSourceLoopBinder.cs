#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{AudioSource, bool}"/> that sets the <see cref="AudioSource.loop"/> property.
    /// </summary>
    /// <include file="XmlExampleDoc-AudioSource-Loop-1.1.0.xml" path="doc//member[@name='AudioSourceLoopBinder']/*" />
    [Serializable]
    public class AudioSourceLoopBinder : TargetBinder<AudioSource, bool>
    {
        /// <inheritdoc/>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/>.</exception>
        public AudioSourceLoopBinder(AudioSource target, IConverter<bool, bool>? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }

        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => Target.loop;
            set => Target.loop = value;
        }
    }
}