using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{ParticleSystem}"/> that binds
    /// <see cref="ParticleSystem.EmissionModule.rateOverTime"/>.
    /// </summary>
    /// <remarks>
    /// Writes the module's multiplier rather than replacing the curve, so a rate authored as a curve keeps its shape
    /// and scales by the bound value. Negative and non-finite values are clamped to zero.
    /// </remarks>
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
                // emission is a struct wrapper; write through a local copy since the property can't be accessed by ref.
                var emission = CachedComponent.emission;
                emission.rateOverTimeMultiplier = this.SafeClamp(value, 0f, float.MaxValue);
            }
        }
    }
}
