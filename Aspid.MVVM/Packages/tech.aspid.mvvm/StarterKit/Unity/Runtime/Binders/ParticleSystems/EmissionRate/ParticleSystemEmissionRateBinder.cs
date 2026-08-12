#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetFloatBinder{ParticleSystem}"/> that binds
    /// <see cref="ParticleSystem.EmissionModule.rateOverTime"/>.
    /// </summary>
    /// <inheritdoc cref="ParticleSystemEmissionRateMonoBinder"/>
    [Serializable]
    public class ParticleSystemEmissionRateBinder : TargetFloatBinder<ParticleSystem>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => Target.emission.rateOverTimeMultiplier;
            set
            {
                // Модуль — структура-обёртка над самой системой: запись через локальную копию доходит до системы,
                // а обратиться к свойству модуля напрямую язык не даёт (emission — свойство, а не поле).
                var emission = Target.emission;
                emission.rateOverTimeMultiplier = BinderMath.SafeClamp(value, 0f, float.MaxValue);
            }
        }

        /// <inheritdoc/>
        public ParticleSystemEmissionRateBinder(
            ParticleSystem target,
            IConverter<float, float>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }
    }
}
