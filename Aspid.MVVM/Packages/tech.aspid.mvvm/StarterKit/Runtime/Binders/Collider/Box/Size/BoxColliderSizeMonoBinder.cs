using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent, TProperty}"/> that binds <see cref="BoxCollider.size"/>.
    /// </summary>
    /// <remarks>
    /// Negative components are raised to zero.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(BoxCollider), serializePropertyNames: "m_Size")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Box/BoxCollider Binder – Size")]
    public class BoxColliderSizeMonoBinder : ComponentMonoBinder<BoxCollider, Vector3>, IVector3Binder
    {
        /// <inheritdoc/>
        protected sealed override Vector3 Property
        {
            get => CachedComponent.size;
            set => CachedComponent.size = this.NonNegative(value);
        }
    }
}
