#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetFloatBinder{Light}"/> that binds <see cref="Light.spotAngle"/>.
    /// </summary>
    /// <remarks>
    /// Unity clamps the angle to 1–179 degrees on its own; non-finite values are dropped instead of being written.
    /// </remarks>
    [Serializable]
    public class LightSpotAngleBinder : TargetFloatBinder<Light>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => Target.spotAngle;
            set
            {
                if (!BinderMath.IsFinite(value)) return;
                Target.spotAngle = value;
            }
        }

        /// <inheritdoc/>
        public LightSpotAngleBinder(
            Light target,
            IConverter<float, float>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }
    }
}
