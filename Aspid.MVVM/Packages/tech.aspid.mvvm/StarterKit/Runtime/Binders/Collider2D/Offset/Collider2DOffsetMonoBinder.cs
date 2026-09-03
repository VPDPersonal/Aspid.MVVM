using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent, TProperty}"/> that binds <see cref="Collider2D.offset"/>.
    /// </summary>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(Collider2D), serializePropertyNames: "m_Offset")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider2D/Collider2D Binder – Offset")]
    public class Collider2DOffsetMonoBinder : ComponentMonoBinder<Collider2D, Vector2>, IVector2Binder
    {
        /// <inheritdoc/>
        protected sealed override Vector2 Property
        {
            get => CachedComponent.offset;
            set
            {
                if (this.RequireFinite(value))
                    CachedComponent.offset = value;
            }
        }
    }
}
