using UnityEngine;
using PhysicsMaterial = UnityEngine.PhysicsMaterial;
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.PhysicsMaterial, UnityEngine.PhysicsMaterial>;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{T1, T2, T3}">ComponentMonoBinder&lt;Collider, PhysicsMaterial, IConverter&lt;PhysicsMaterial, PhysicsMaterial&gt;&gt;</see> that binds the <see cref="Collider.material"/> property.
    /// </summary>
    /// <remarks>
    /// Reads back <see cref="Collider.sharedMaterial"/>, not <see cref="Collider.material"/> — reading the latter
    /// would replace the asset with a private clone and break equality with what the ViewModel sent.
    /// </remarks>
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Collider Binder – Material")]
    [AddBinderContextMenu(typeof(Collider), serializePropertyNames: "m_Material")]
    public class ColliderMaterialMonoBinder : ComponentMonoBinder<Collider, PhysicsMaterial, Converter>
    {
        /// <inheritdoc/>
        protected sealed override PhysicsMaterial Property
        {
            get => CachedComponent.sharedMaterial;
            set => CachedComponent.material = value;
        }
    }
}