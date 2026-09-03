using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentObjectMonoBinder{TComponent, TObject}"/> that binds <see cref="Collider2D.sharedMaterial"/>.
    /// </summary>
    /// <remarks>
    /// Uses <see cref="Collider2D.sharedMaterial"/>: <c>material</c> would clone the asset on read.
    /// </remarks>
    [GenerateSerializableBinder]
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
