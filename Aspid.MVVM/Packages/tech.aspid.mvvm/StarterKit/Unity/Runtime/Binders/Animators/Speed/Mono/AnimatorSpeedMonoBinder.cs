using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{Animator}"/> that binds <see cref="Animator.speed"/>.
    /// </summary>
    /// <remarks>
    /// Negative values play backwards and are kept. Only a non-finite value is refused: the animator accepts
    /// one and then does not advance at all, with nothing in the log.
    /// </remarks>
    [AddBinderContextMenu(typeof(Animator))]
    [AddComponentMenu("Aspid/MVVM/Binders/Animator/Animator Binder – Speed")]
    public class AnimatorSpeedMonoBinder : ComponentFloatMonoBinder<Animator>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => CachedComponent.speed;
            set
            {
                if (!BinderMath.IsFinite(value)) return;
                CachedComponent.speed = value;
            }
        }
    }
}
