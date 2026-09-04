using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{TComponent}"/> that binds <see cref="Rigidbody2D.gravityScale"/>.
    /// </summary>
    /// <remarks>
    /// Unity itself rejects a non-finite scale.
    /// </remarks>
    [GenerateSerializableBinder]
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
