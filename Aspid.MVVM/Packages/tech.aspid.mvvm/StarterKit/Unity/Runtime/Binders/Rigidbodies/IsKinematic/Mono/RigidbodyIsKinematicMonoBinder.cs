using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentBoolMonoBinder{Rigidbody}"/> that binds <see cref="Rigidbody.isKinematic"/>.
    /// </summary>
    [AddBinderContextMenu(typeof(Rigidbody), serializePropertyNames: "m_IsKinematic")]
    [AddComponentMenu("Aspid/MVVM/Binders/Physics/Rigidbody Binder – Is Kinematic")]
    public class RigidbodyIsKinematicMonoBinder : ComponentBoolMonoBinder<Rigidbody>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => CachedComponent.isKinematic;
            set => CachedComponent.isKinematic = value;
        }
    }
}
