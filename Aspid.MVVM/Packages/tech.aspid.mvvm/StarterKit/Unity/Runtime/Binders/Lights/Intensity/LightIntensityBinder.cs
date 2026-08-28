#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetFloatBinder{Light}"/> that binds <see cref="Light.intensity"/>.
    /// </summary>
    /// <remarks>Non-finite values are dropped instead of being written.</remarks>
    [Serializable]
    public class LightIntensityBinder : TargetFloatBinder<Light>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => Target.intensity;
            set
            {
                if (!this.RequireFinite(value, Target)) return;
                Target.intensity = value;
            }
        }

        /// <inheritdoc/>
        public LightIntensityBinder(
            Light target,
            IConverter<float, float>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }
    }
}
