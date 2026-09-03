using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent, TProperty}"/> that binds <see cref="BoxCollider2D.size"/>.
    /// </summary>
    /// <remarks>
    /// Negative components are raised to zero.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(BoxCollider2D), serializePropertyNames: "m_Size")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider2D/Box/BoxCollider2D Binder – Size")]
    public class BoxCollider2DSizeMonoBinder : ComponentMonoBinder<BoxCollider2D, Vector2>, IVector2Binder
    {
        /// <inheritdoc/>
        protected sealed override Vector2 Property
        {
            get => CachedComponent.size;
            set => CachedComponent.size = this.NonNegative(value);
        }
    }
}
