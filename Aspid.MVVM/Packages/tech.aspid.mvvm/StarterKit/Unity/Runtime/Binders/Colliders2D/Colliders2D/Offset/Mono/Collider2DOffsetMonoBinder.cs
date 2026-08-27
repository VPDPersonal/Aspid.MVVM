using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentVector2MonoBinder{Collider2D}"/> that binds <see cref="Collider2D.offset"/>.
    /// </summary>
    /// <remarks>
    /// Negative offsets are ordinary, so only a non-finite value is refused, leaving the offset unchanged.
    /// </remarks>
    [AddBinderContextMenu(typeof(Collider2D), serializePropertyNames: "m_Offset")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider2D/Collider2D Binder – Offset")]
    public class Collider2DOffsetMonoBinder : ComponentVector2MonoBinder<Collider2D>
    {
        /// <inheritdoc/>
        protected sealed override Vector2 Property
        {
            get => CachedComponent.offset;
            set
            {
                if (!BinderMath.IsFinite(value.x) || !BinderMath.IsFinite(value.y)) return;
                CachedComponent.offset = value;
            }
        }
    }
}
