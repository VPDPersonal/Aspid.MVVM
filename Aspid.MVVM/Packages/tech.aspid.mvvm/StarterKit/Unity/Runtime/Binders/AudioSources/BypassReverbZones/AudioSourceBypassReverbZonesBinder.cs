#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBoolBinder{AudioSource}"/> that sets the <see cref="AudioSource.bypassReverbZones"/> property.
    /// </summary>
    /// <include file="XmlExampleDoc-AudioSource-BypassReverbZones-1.1.0.xml" path="doc//member[@name='AudioSourceBypassReverbZonesBinder']/*" />
    [Serializable]
    public class AudioSourceBypassReverbZonesBinder : TargetBoolBinder<AudioSource>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => Target.bypassReverbZones;
            set => Target.bypassReverbZones = value;
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/>.</exception>
        public AudioSourceBypassReverbZonesBinder(AudioSource target, IConverter<bool, bool>? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}