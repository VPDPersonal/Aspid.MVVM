#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetFloatBinder{Light}"/> that binds <see cref="Light.intensity"/>.
    /// </summary>
    /// <remarks>
    /// How bright the lamp burns. A non-finite value is dropped rather than written. Unity clamps the range on its
    /// own, so nothing else needs guarding here, but it stores <see cref="float.NaN"/> verbatim — and a NaN in a
    /// rendering number does not fail loudly, it just makes the image wrong in a way that points nowhere near the
    /// ViewModel that produced it.
    /// </remarks>
    [Serializable]
    public class LightIntensityBinder : TargetFloatBinder<Light>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => Target.intensity;
            set
            {
                if (!BinderMath.IsFinite(value)) return;
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
