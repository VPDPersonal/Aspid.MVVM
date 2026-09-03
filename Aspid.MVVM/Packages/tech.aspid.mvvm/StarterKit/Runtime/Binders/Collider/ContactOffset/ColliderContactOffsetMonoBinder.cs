using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{TComponent}"/> that binds <see cref="Collider.contactOffset"/>.
    /// </summary>
    /// <remarks>
    /// Clamped to a small positive minimum: Unity rejects a zero contact offset.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(Collider))]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Collider Binder – Contact Offset")]
    public class ColliderContactOffsetMonoBinder : ComponentFloatMonoBinder<Collider>
    {
        private const float MinimumContactOffset = 0.0001f;

        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => CachedComponent.contactOffset;
            set => CachedComponent.contactOffset = this.SafeClamp(value, MinimumContactOffset, float.MaxValue);
        }
    }
}
