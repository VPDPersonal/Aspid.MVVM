#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetIntBinder{AudioSource}"/> that sets the <see cref="AudioSource.timeSamples"/> property.
    /// </summary>
    /// <include file="XmlExampleDoc-AudioSource-TimeSamples-1.1.0.xml" path="doc//member[@name='AudioSourceTimeSamplesBinder']/*" />
    [Serializable]
    public class AudioSourceTimeSamplesBinder : TargetIntBinder<AudioSource>
    {
        /// <inheritdoc/>
        protected sealed override int Property
        {
            get => Target.timeSamples;
            set => Target.timeSamples = value;
        }

        /// <inheritdoc />
        public AudioSourceTimeSamplesBinder(AudioSource target, IConverter<int, int>? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}