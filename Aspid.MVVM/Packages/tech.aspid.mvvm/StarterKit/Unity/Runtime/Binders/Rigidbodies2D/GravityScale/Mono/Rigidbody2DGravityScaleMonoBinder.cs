using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{Rigidbody2D}"/> that binds <see cref="Rigidbody2D.gravityScale"/>.
    /// </summary>
    /// <remarks>
    /// How strongly gravity pulls this body, where <c>0</c> suspends it and a negative value inverts it — so
    /// nothing is clamped. Unity refuses a non-finite scale on its own.
    /// </remarks>
    [AddBinderContextMenu(typeof(Rigidbody2D), serializePropertyNames: "m_GravityScale")]
    [AddComponentMenu("Aspid/MVVM/Binders/Physics/Rigidbody2D Binder – Gravity Scale")]
    public class Rigidbody2DGravityScaleMonoBinder : ComponentFloatMonoBinder<Rigidbody2D>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => CachedComponent.gravityScale;
            set => CachedComponent.gravityScale = value;
        }
    }
}
