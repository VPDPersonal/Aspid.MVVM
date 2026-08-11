using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{CircleCollider2D}"/> that binds <see cref="CircleCollider2D.radius"/>.
    /// </summary>
    /// <remarks>
    /// The 2D counterpart of <see cref="SphereCollider.radius"/>, which the package already bound — an
    /// explosion radius, a pickup range, a shield that grows. Clamped non-negative.
    /// </remarks>
    [AddBinderContextMenu(typeof(CircleCollider2D), serializePropertyNames: "m_Radius")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider2D/Circle/CircleCollider2D Binder – Radius")]
    public class CircleCollider2DRadiusMonoBinder : ComponentFloatMonoBinder<CircleCollider2D>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => CachedComponent.radius;
            set => CachedComponent.radius = BinderMath.SafeClamp(value, 0f, float.MaxValue);
        }
    }
}
