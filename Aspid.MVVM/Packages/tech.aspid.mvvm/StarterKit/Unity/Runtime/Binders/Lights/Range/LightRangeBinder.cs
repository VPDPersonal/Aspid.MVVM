#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetFloatBinder{Light}"/> that binds <see cref="Light.range"/>.
    /// </summary>
    /// <remarks>
    /// Non-finite values are dropped instead of the zero Unity would otherwise coerce them to, which would
    /// switch the light off.
    /// </remarks>
    [Serializable]
    public class LightRangeBinder : TargetFloatBinder<Light>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => Target.range;
            set
            {
                if (!this.RequireFinite(value, Target)) return;
                Target.range = value;
            }
        }

        /// <inheritdoc/>
        public LightRangeBinder(
            Light target,
            IConverter<float, float>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }
    }
}
