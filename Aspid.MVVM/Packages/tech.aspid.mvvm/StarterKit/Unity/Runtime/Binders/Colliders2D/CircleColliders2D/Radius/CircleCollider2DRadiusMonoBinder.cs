using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{CircleCollider2D}"/> that binds <see cref="CircleCollider2D.radius"/>.
    /// </summary>
    /// <remarks>
    /// Clamped non-negative; a non-finite value maps to <c>0</c>.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(CircleCollider2D), serializePropertyNames: "m_Radius")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider2D/Circle/CircleCollider2D Binder – Radius")]
    public class CircleCollider2DRadiusMonoBinder : ComponentFloatMonoBinder<CircleCollider2D>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => CachedComponent.radius;
            set => CachedComponent.radius = this.SafeClamp(value, 0f, float.MaxValue);
        }
    }
}
