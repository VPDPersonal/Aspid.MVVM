#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetFloatBinder{Rigidbody}"/> that binds <see cref="Rigidbody.mass"/>.
    /// </summary>
    /// <remarks>A non-finite value is ignored, keeping the last mass that was successfully applied.</remarks>
    [Serializable]
    public class RigidbodyMassBinder : TargetFloatBinder<Rigidbody>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => Target.mass;
            set
            {
                if (!this.RequireFinite(value, Target)) return;
                Target.mass = value;
            }
        }

        /// <inheritdoc/>
        public RigidbodyMassBinder(
            Rigidbody target,
            IConverter<float, float>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }
    }
}
