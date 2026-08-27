using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{Collider}"/> that binds <see cref="Collider.contactOffset"/>.
    /// </summary>
    /// <remarks>
    /// Clamped to a small positive minimum instead of zero — Unity rejects a zero contact offset and logs an error.
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
