using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentObjectMonoBinder{T1, T2}">ComponentObjectMonoBinder&lt;Collider2D, PhysicsMaterial2D&gt;</see> that binds
    /// <see cref="Collider2D.sharedMaterial"/>.
    /// </summary>
    /// <remarks>
    /// Friction and bounce as one swappable asset: ice, mud, rubber. The 3D domain had this binder and the 2D one had
    /// nothing at all.
    /// <para/>
    /// Reads and writes <see cref="Collider2D.sharedMaterial"/> rather than <c>material</c>, which instantiates a copy
    /// on read and leaks it into the scene. Writing the shared asset does affect every collider using it, which is
    /// what swapping a surface material is meant to do.
    /// <para/>
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established, the current material is sent back
    /// to the ViewModel — as <see langword="null"/> if it has been destroyed.
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
