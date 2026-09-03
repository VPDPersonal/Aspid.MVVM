using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{TComponent}"/> that binds <see cref="Collider2D.density"/>.
    /// </summary>
    /// <remarks>
    /// A negative value is raised to zero. Unity ignores the write unless the <see cref="Rigidbody2D"/> has
    /// <see cref="Rigidbody2D.useAutoMass"/> enabled.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(Collider2D), serializePropertyNames: "m_Density")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider2D/Collider2D Binder – Density")]
    public class Collider2DDensityMonoBinder : ComponentFloatMonoBinder<Collider2D>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => CachedComponent.density;
            set => CachedComponent.density = this.NonNegative(value);
        }
    }
}
