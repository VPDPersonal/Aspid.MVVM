using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentBoolMonoBinder{ParticleSystem}"/> that binds <see cref="ParticleSystem.EmissionModule.enabled"/>.
    /// </summary>
    /// <remarks>
    /// Turns emission off without stopping the system: the particles already alive keep going and the effect
    /// resumes the instant it is turned back on, with no restart. That is what a continuous effect wants — a
    /// thruster, rain, a torch — where <c>Stop</c> and <c>Play</c> would end and re-begin it instead.
    /// <para/>
    /// A module is a struct, but one holding a handle to the system rather than a copy of its data, so writing
    /// through a local goes to the system itself. The local is why this cannot be an expression-bodied setter.
    /// </remarks>
    [AddBinderContextMenu(typeof(ParticleSystem))]
    [AddComponentMenu("Aspid/MVVM/Binders/Effects/ParticleSystem/ParticleSystem Binder – Emission Enabled")]
    public class ParticleSystemEmissionEnabledMonoBinder : ComponentBoolMonoBinder<ParticleSystem>
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
