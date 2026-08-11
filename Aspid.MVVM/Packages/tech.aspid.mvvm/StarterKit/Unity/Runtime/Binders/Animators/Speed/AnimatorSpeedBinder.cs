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
    /// How fast the whole animator runs. It is the property behind slow motion, a wind-up, a hasted or a
    /// frozen character — and the domain bound the parameters an animator reads and not the clock it reads
    /// them on.
    /// <para/>
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
                // Отрицательная скорость — воспроизведение назад, это законно. Отбрасывается только нефинитное:
                // Animator принимает NaN и после этого не двигается вообще, ничего не сообщая.
                if (!BinderMath.IsFinite(value)) return;
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
