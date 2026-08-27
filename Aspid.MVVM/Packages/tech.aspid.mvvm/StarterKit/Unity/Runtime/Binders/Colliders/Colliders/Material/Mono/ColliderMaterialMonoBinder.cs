using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinderWithConverter{T1, T2}">ComponentMonoBinderWithConverter&lt;Collider, PhysicsMaterial&gt;</see> that binds the <see cref="Collider.material"/> property.
    /// </summary>
    /// <remarks>
    /// Reads back <see cref="Collider.sharedMaterial"/>, not <see cref="Collider.material"/> — reading the latter
    /// would replace the asset with a private clone and break equality with what the ViewModel sent.
    /// </remarks>
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Collider Binder – Material")]
    [AddBinderContextMenu(typeof(Collider), serializePropertyNames: "m_Material")]
    public class ColliderMaterialMonoBinder : ComponentMonoBinderWithConverter<Collider, PhysicsMaterial>
    {
        /// <inheritdoc/>
        protected sealed override PhysicsMaterial Property
        {
            get => CachedComponent.sharedMaterial;
            set => CachedComponent.material = value;
        }
    }
}