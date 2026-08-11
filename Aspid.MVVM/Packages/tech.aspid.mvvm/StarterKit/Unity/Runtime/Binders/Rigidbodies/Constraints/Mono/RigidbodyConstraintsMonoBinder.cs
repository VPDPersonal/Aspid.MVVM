using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{T1, T2}">ComponentMonoBinder&lt;Rigidbody, RigidbodyConstraints&gt;</see> that binds
    /// <see cref="Rigidbody.constraints"/>.
    /// </summary>
    /// <remarks>
    /// The one property of a body a ViewModel changes at runtime rather than authoring once: pinning a ragdoll,
    /// locking a door on its hinge, holding a 3D body in a 2D plane. <see cref="RigidbodyConstraints"/> is a flag
    /// enum, so the ViewModel sends the whole mask — an individual axis is a combination, not a separate binder.
    /// <para/>
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established, the current mask is sent back
    /// to the ViewModel.
    /// </remarks>
    [AddBinderContextMenu(typeof(Rigidbody))]
    [AddComponentMenu("Aspid/MVVM/Binders/Physics/Rigidbody Binder – Constraints")]
    public class RigidbodyConstraintsMonoBinder : ComponentMonoBinder<Rigidbody, RigidbodyConstraints>
    {
        /// <inheritdoc/>
        protected sealed override RigidbodyConstraints Property
        {
            get => CachedComponent.constraints;
            set => CachedComponent.constraints = value;
        }
    }
}
