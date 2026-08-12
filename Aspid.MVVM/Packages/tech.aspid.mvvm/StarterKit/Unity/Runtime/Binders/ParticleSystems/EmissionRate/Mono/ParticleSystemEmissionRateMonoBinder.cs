using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{ParticleSystem}"/> that binds
    /// <see cref="ParticleSystem.EmissionModule.rateOverTime"/>.
    /// </summary>
    /// <remarks>
    /// How much of an effect there is, as opposed to whether it runs at all: rain that thickens, a fire that grows,
    /// a thruster that answers the throttle. <see cref="ParticleSystemEmissionEnabledMonoBinder"/> can only turn the
    /// emission off, and restarting a system to change its density loses the particles already alive.
    /// <para/>
    /// Writes the module's multiplier rather than replacing the curve, so a rate authored as a curve keeps its shape
    /// and is scaled by the bound value. Negative rates are clamped to zero, which is also where a non-finite value
    /// lands — Unity stores one verbatim and the system then emits nothing until the value is replaced.
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
                // Модуль — структура-обёртка над самой системой: запись через локальную копию доходит до системы,
                // а обратиться к свойству модуля напрямую язык не даёт (emission — свойство, а не поле).
                var emission = CachedComponent.emission;
                emission.rateOverTimeMultiplier = BinderMath.SafeClamp(value, 0f, float.MaxValue);
            }
        }
    }
}
