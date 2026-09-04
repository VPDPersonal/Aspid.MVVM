using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent, TProperty}"/> that binds
    /// <see cref="ParticleSystem.EmissionModule.enabled"/>.
    /// </summary>
    /// <remarks>
    /// Alive particles keep going; emission resumes instantly when re-enabled.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(ParticleSystem))]
    [AddComponentMenu("Aspid/MVVM/Binders/Effects/ParticleSystem/ParticleSystem Binder – Emission Enabled")]
    public class ParticleSystemEmissionEnabledMonoBinder : ComponentMonoBinder<ParticleSystem, bool>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => CachedComponent.emission.enabled;
            set
            {
                var emission = CachedComponent.emission;
                emission.enabled = value;
            }
        }
    }
}
