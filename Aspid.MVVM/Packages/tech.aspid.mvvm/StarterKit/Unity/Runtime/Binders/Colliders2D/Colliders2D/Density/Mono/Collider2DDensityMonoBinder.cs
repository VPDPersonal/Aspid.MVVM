using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{Collider2D}"/> that binds <see cref="Collider2D.density"/>.
    /// </summary>
    /// <remarks>
    /// Clamped non-negative; a non-finite value maps to <c>0</c>.
    /// <para/>
    /// Unity ignores the write unless the attached <see cref="Rigidbody2D"/> has
    /// <see cref="Rigidbody2D.useAutoMass"/> enabled, silently keeping the previous value.
    /// </remarks>
    [AddBinderContextMenu(typeof(Collider2D), serializePropertyNames: "m_Density")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider2D/Collider2D Binder – Density")]
    public class Collider2DDensityMonoBinder : ComponentFloatMonoBinder<Collider2D>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => CachedComponent.density;
            set => CachedComponent.density = this.SafeClamp(value, 0f, float.MaxValue);
        }
    }
}
