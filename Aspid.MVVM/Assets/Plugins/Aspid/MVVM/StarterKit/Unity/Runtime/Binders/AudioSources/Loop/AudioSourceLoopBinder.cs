#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBoolBinder{AudioSource}"/> that sets the <see cref="AudioSource.loop"/> property.
    /// </summary>
    /// <include file="XmlExampleDoc-AudioSource-Loop-1.1.0.xml" path="doc//member[@name='AudioSourceLoopBinder']/*" />
    [Serializable]
    public class AudioSourceLoopBinder : TargetBoolBinder<AudioSource>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => Target.loop;
            set => Target.loop = value;
        }

        /// <inheritdoc />
        public AudioSourceLoopBinder(AudioSource target, bool isInvert = false, BindMode mode = BindMode.OneWay)
            : base(target, isInvert, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}