using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentVector2MonoBinder{Collider2D}"/> that binds <see cref="Collider2D.offset"/>.
    /// </summary>
    /// <remarks>
    /// Where the collider sits relative to its transform — what a crouch, a duck or a sprite that leans
    /// changes without moving the object. Negative offsets are ordinary, so only a non-finite value is
    /// refused.
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
                // Смещение осмысленно отрицательное, поэтому отбрасывается только нефинитное значение:
                // NaN здесь уводит коллайдер в никуда, и физика об этом не сообщает.
                if (!BinderMath.IsFinite(value.x) || !BinderMath.IsFinite(value.y)) return;
                CachedComponent.offset = value;
            }
        }
    }
}
