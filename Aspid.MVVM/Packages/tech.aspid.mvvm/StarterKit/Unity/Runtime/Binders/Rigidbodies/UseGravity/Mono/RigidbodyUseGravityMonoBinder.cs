using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentBoolMonoBinder{Rigidbody}"/> that binds <see cref="Rigidbody.useGravity"/>.
    /// </summary>
    /// <remarks>
    /// Suspending gravity for a held or hovering object is a state a ViewModel usually owns, and it had no binder.
    /// </remarks>
    [AddBinderContextMenu(typeof(Rigidbody), serializePropertyNames: "m_UseGravity")]
    [AddComponentMenu("Aspid/MVVM/Binders/Physics/Rigidbody Binder – Use Gravity")]
    public class RigidbodyUseGravityMonoBinder : ComponentBoolMonoBinder<Rigidbody>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => CachedComponent.useGravity;
            set => CachedComponent.useGravity = value;
        }
    }
}
