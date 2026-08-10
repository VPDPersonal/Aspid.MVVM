#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetFloatBinder<Light>"/> that binds <see cref="Light.range"/>.
    /// </summary>
    /// <remarks>
    /// How far a point or spot light reaches; a directional light ignores it. Unity maps a non-finite range to
    /// zero, which switches the lamp off — dropping the write keeps the last range that lit something instead.
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
                if (!BinderMath.IsFinite(value)) return;
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
