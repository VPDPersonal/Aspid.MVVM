using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentObjectMonoBinder{T1, T2}">ComponentObjectMonoBinder&lt;Collider2D, PhysicsMaterial2D&gt;</see> that binds
    /// <see cref="Collider2D.sharedMaterial"/>.
    /// </summary>
    /// <remarks>
    /// Reads and writes <see cref="Collider2D.sharedMaterial"/> rather than <c>material</c>, which instantiates a
    /// copy on read and leaks it into the scene.
    /// <para/>
    /// The value sent back to the ViewModel in <see cref="BindMode.OneWayToSource"/> is
    /// <see langword="null"/> if the material has been destroyed.
    /// </remarks>
    [AddBinderContextMenu(typeof(Collider2D), serializePropertyNames: "m_Material")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider2D/Collider2D Binder – Material")]
    public class Collider2DMaterialMonoBinder : ComponentObjectMonoBinder<Collider2D, PhysicsMaterial2D>
    {
        /// <inheritdoc/>
        protected sealed override PhysicsMaterial2D Property
        {
            get => CachedComponent.sharedMaterial;
            set => CachedComponent.sharedMaterial = value;
        }
    }
}
