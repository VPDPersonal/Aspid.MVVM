#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetFloatBinder{Rigidbody2D}"/> that binds <see cref="Rigidbody2D.mass"/>.
    /// </summary>
    /// <remarks>A non-finite value is ignored, keeping the last mass that was successfully applied.</remarks>
    [Serializable]
    public class Rigidbody2DMassBinder : TargetFloatBinder<Rigidbody2D>
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
        public Rigidbody2DMassBinder(
            Rigidbody2D target,
            IConverter<float, float>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }
    }
}
