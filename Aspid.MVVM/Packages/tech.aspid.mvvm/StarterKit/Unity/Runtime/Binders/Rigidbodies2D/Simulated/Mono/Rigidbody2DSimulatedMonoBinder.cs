using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentBoolMonoBinder{Rigidbody2D}"/> that binds <see cref="Rigidbody2D.simulated"/>.
    /// </summary>
    /// <remarks>
    /// Takes the body out of the simulation together with its colliders — cheaper than disabling the object when
    /// only physics should pause.
    /// </remarks>
    [AddBinderContextMenu(typeof(Rigidbody2D), serializePropertyNames: "m_Simulated")]
    [AddComponentMenu("Aspid/MVVM/Binders/Physics/Rigidbody2D Binder – Simulated")]
    public class Rigidbody2DSimulatedMonoBinder : ComponentBoolMonoBinder<Rigidbody2D>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => CachedComponent.simulated;
            set => CachedComponent.simulated = value;
        }
    }
}
