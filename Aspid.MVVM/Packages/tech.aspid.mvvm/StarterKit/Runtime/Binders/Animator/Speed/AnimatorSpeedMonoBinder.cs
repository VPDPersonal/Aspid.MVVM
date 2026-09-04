using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{TComponent}"/> that binds <see cref="Animator.speed"/>.
    /// </summary>
    /// <remarks>
    /// Negative values play backwards and are kept; a non-finite value is refused.
    /// </remarks>
    [GenerateSerializableBinder]
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
                if (this.RequireFinite(value))
                    CachedComponent.speed = value;
            }
        }
    }
}
