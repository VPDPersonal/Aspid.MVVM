#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetFloatBinder{AudioSource}"/> that sets the <see cref="AudioSource.time"/> property.
    /// </summary>
    /// <include file="XmlExampleDoc-AudioSource-Time-1.1.0.xml" path="doc//member[@name='AudioSourceTimeBinder']/*" />
    [Serializable]
    public class AudioSourceTimeBinder : TargetFloatBinder<AudioSource>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => Target.time;
            set => Target.time = value;
        }

        /// <inheritdoc />
        public AudioSourceTimeBinder(AudioSource target, IConverter<float, float>? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}