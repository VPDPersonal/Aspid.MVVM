using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{Collider2D}"/> that binds <see cref="Collider2D.density"/>.
    /// </summary>
    /// <remarks>
    /// How heavy the shape is, when the body computes its mass from its colliders — a crate that fills with
    /// water, a balloon that deflates. Clamped non-negative.
    /// <para/>
    /// Unity <em>ignores</em> the write unless the attached <see cref="Rigidbody2D"/> has
    /// <see cref="Rigidbody2D.useAutoMass"/> enabled — the property keeps its previous value and nothing is logged.
    /// Bind the density only on a body that computes its mass from its colliders.
    /// </remarks>
    [AddBinderContextMenu(typeof(Collider2D), serializePropertyNames: "m_Density")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider2D/Collider2D Binder – Density")]
    public class Collider2DDensityMonoBinder : ComponentFloatMonoBinder<Collider2D>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => CachedComponent.density;
            set => CachedComponent.density = BinderMath.SafeClamp(value, 0f, float.MaxValue);
        }
    }
}
