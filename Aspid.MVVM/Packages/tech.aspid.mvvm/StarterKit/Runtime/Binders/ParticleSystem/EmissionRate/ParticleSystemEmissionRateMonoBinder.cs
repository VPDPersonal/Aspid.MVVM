using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{TComponent}"/> that binds
    /// <see cref="ParticleSystem.EmissionModule.rateOverTimeMultiplier"/>.
    /// </summary>
    /// <remarks>
    /// The multiplier scales an authored curve instead of replacing it. A negative value is raised to zero.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(ParticleSystem), serializePropertyNames: "rateOverTime")]
    [AddComponentMenu("Aspid/MVVM/Binders/Effects/ParticleSystem/ParticleSystem Binder – Emission Rate")]
    public class ParticleSystemEmissionRateMonoBinder : ComponentFloatMonoBinder<ParticleSystem>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => CachedComponent.emission.rateOverTimeMultiplier;
            set
            {
                var emission = CachedComponent.emission;
                emission.rateOverTimeMultiplier = this.NonNegative(value);
            }
        }
    }
}
