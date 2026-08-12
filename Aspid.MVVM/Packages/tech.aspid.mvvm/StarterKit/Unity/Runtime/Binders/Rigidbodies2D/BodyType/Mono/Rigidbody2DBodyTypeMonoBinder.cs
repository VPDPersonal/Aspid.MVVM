using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{T1, T2}">ComponentMonoBinder&lt;Rigidbody2D, RigidbodyType2D&gt;</see> that binds
    /// <see cref="Rigidbody2D.bodyType"/>.
    /// </summary>
    /// <remarks>
    /// The 2D counterpart of <see cref="Rigidbody.isKinematic"/>, and wider than it:
    /// <see cref="RigidbodyType2D.Static"/> takes the body out of the simulation entirely, which is what a platform
    /// that stops moving wants. <see cref="Rigidbody2D.simulated"/> answers a different question — whether the body
    /// is simulated at all — so both binders exist side by side.
    /// <para/>
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established, the current body type is sent
    /// back to the ViewModel.
    /// </remarks>
    [AddBinderContextMenu(typeof(Rigidbody2D), serializePropertyNames: "m_BodyType")]
    [AddComponentMenu("Aspid/MVVM/Binders/Physics/Rigidbody2D Binder – Body Type")]
    public class Rigidbody2DBodyTypeMonoBinder : ComponentMonoBinder<Rigidbody2D, RigidbodyType2D>
    {
        /// <inheritdoc/>
        protected sealed override RigidbodyType2D Property
        {
            get => CachedComponent.bodyType;
            set => CachedComponent.bodyType = value;
        }
    }
}
