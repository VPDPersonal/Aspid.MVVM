#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetFloatBinder{Animator}"/> that binds <see cref="Animator.speed"/>.
    /// </summary>
    /// <remarks>
    /// Negative values play backwards and are kept. Only a non-finite value is refused: the animator accepts
    /// one and then does not advance at all, with nothing in the log.
    /// </remarks>
    [Serializable]
    public class AnimatorSpeedBinder : TargetFloatBinder<Animator>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => Target.speed;
            set
            {
                if (!this.RequireFinite(value, Target)) return;
                Target.speed = value;
            }
        }

        /// <inheritdoc/>
        public AnimatorSpeedBinder(
            Animator target,
            IConverter<float, float>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }
    }
}
