#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{ParticleSystem, Color}"/> that binds
    /// <see cref="ParticleSystem.MainModule.startColor"/>.
    /// </summary>
    /// <inheritdoc cref="ParticleSystemStartColorMonoBinder"/>
    [Serializable]
    public class ParticleSystemStartColorBinder : TargetBinder<ParticleSystem, Color>, IColorBinder
    {
        /// <inheritdoc/>
        protected sealed override Color Property
        {
            get => Target.main.startColor.color;
            set
            {
                // main is a struct wrapper; write through a local copy since the property can't be accessed by ref.
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
