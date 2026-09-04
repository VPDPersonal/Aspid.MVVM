using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent, TProperty}"/> that binds <see cref="Rigidbody.isKinematic"/>.
    /// </summary>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(Rigidbody), serializePropertyNames: "m_IsKinematic")]
    [AddComponentMenu("Aspid/MVVM/Binders/Physics/Rigidbody Binder – Is Kinematic")]
    public class RigidbodyIsKinematicMonoBinder : ComponentMonoBinder<Rigidbody, bool>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => CachedComponent.isKinematic;
            set => CachedComponent.isKinematic = value;
        }
    }
}
