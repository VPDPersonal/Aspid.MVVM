using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent, TProperty}"/> that binds <see cref="MeshCollider.cookingOptions"/>.
    /// </summary>
    /// <remarks>
    /// Writing re-cooks the mesh; avoid setting it every frame.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(MeshCollider), serializePropertyNames: "m_CookingOptions")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Mesh/MeshCollider Binder – Cooking Options")]
    public class MeshColliderCookingOptionsMonoBinder : ComponentMonoBinder<MeshCollider, MeshColliderCookingOptions>
    {
        /// <inheritdoc/>
        protected sealed override MeshColliderCookingOptions Property
        {
            get => CachedComponent.cookingOptions;
            set => CachedComponent.cookingOptions = value;
        }
    }
}
