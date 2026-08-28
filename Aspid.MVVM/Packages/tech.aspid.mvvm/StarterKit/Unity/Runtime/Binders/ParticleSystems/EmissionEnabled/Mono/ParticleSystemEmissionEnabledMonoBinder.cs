using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent,TProperty}"/> that binds <see cref="ParticleSystem.EmissionModule.enabled"/>.
    /// </summary>
    /// <remarks>
    /// Turns emission off without stopping the system: particles already alive keep going, and emission resumes
    /// instantly when turned back on, unlike a <c>Stop</c>/<c>Play</c> restart.
    /// </remarks>
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
