#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetColorBinder{ParticleSystem}"/> that binds
    /// <see cref="ParticleSystem.MainModule.startColor"/>.
    /// </summary>
    /// <inheritdoc cref="ParticleSystemStartColorMonoBinder"/>
    [Serializable]
    public class ParticleSystemStartColorBinder : TargetColorBinder<ParticleSystem>
    {
        /// <inheritdoc/>
        protected sealed override Color Property
        {
            get => Target.main.startColor.color;
            set
            {
                // Модуль — структура-обёртка над самой системой: запись через локальную копию доходит до системы,
                // а обратиться к свойству модуля напрямую язык не даёт (main — свойство, а не поле).
                var main = Target.main;
                main.startColor = value;
            }
        }

        /// <inheritdoc/>
        public ParticleSystemStartColorBinder(
            ParticleSystem target,
            IConverter<Color, Color>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }
    }
}
