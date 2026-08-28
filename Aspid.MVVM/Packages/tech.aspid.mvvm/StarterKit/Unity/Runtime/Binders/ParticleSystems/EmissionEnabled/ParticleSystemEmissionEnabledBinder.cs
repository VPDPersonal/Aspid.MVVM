#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{ParticleSystem, bool}"/> that binds <see cref="ParticleSystem.EmissionModule.enabled"/>.
    /// </summary>
    /// <inheritdoc cref="ParticleSystemEmissionEnabledMonoBinder"/>
    [Serializable]
    public class ParticleSystemEmissionEnabledBinder : TargetBinder<ParticleSystem, bool>
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
            IConverter<bool, bool>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }
    }
}
