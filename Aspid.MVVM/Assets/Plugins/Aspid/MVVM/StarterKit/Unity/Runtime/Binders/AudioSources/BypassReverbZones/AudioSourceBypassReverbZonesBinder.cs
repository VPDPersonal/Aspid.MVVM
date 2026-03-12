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

        /// <inheritdoc />
        public AudioSourceBypassReverbZonesBinder(AudioSource target, bool isInvert = false, BindMode mode = BindMode.OneWay)
            : base(target, isInvert, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}