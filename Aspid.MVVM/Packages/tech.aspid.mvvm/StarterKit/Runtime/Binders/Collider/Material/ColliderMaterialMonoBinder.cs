using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentObjectMonoBinder{TComponent, TObject}"/> that binds <see cref="Collider.material"/>.
    /// </summary>
    /// <remarks>
    /// Reads back <see cref="Collider.sharedMaterial"/>: reading <see cref="Collider.material"/> would clone the asset.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(Collider), serializePropertyNames: "m_Material")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Collider Binder – Material")]
    public class ColliderMaterialMonoBinder : ComponentObjectMonoBinder<Collider, PhysicsMaterial>
    {
        /// <inheritdoc/>
        protected sealed override PhysicsMaterial Property
        {
            get => CachedComponent.sharedMaterial;
            set => CachedComponent.material = value;
        }
    }
}
