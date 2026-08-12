#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBoolBinder{ParticleSystem}"/> that binds <see cref="ParticleSystem.EmissionModule.enabled"/>.
    /// </summary>
    /// <inheritdoc cref="ParticleSystemEmissionEnabledMonoBinder"/>
    [Serializable]
    public class ParticleSystemEmissionEnabledBinder : TargetBoolBinder<ParticleSystem>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => Target.emission.enabled;
            set
            {
                var emission = Target.emission;
                emission.enabled = value;
            }
        }

        /// <inheritdoc/>
        public ParticleSystemEmissionEnabledBinder(
            ParticleSystem target,
            bool isInvert = false,
            BindMode mode = BindMode.OneWay)
            : base(target, isInvert, mode) { }
    }
}
