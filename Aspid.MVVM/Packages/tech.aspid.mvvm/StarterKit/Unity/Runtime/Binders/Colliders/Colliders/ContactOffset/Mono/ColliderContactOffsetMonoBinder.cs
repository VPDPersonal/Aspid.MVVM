using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{Collider}"/> that binds <see cref="Collider.contactOffset"/>.
    /// </summary>
    /// <remarks>
    /// The distance at which the engine starts treating a pair as touching. Tuning it is how jitter on a
    /// stack of boxes or a wheel resting on a ramp gets fixed, which makes it a value worth exposing to a
    /// difficulty or quality setting.
    /// <para/>
    /// Clamped to a small positive minimum rather than to zero: Unity refuses a contact offset of zero and logs
    /// <c>"Contact offset must be greater than zero"</c> for it, so a bound zero — or a non-finite value landing on a
    /// zero floor — would fill the console instead of taking effect.
    /// </remarks>
    [AddBinderContextMenu(typeof(Collider))]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Collider Binder – Contact Offset")]
    public class ColliderContactOffsetMonoBinder : ComponentFloatMonoBinder<Collider>
    {
        /// <summary>
        /// The smallest offset Unity accepts; it refuses zero and logs an error for it.
        /// </summary>
        private const float MinimumContactOffset = 0.0001f;

        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => CachedComponent.contactOffset;
            set => CachedComponent.contactOffset = BinderMath.SafeClamp(value, MinimumContactOffset, float.MaxValue);
        }
    }
}
