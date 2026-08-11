using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{T1, T2}">ComponentMonoBinder&lt;MeshCollider, MeshColliderCookingOptions&gt;</see> that binds
    /// <see cref="MeshCollider.cookingOptions"/>.
    /// </summary>
    /// <remarks>
    /// What the engine is allowed to clean up while building the collision mesh. A project that swaps meshes
    /// at runtime pays for cooking every time, and this is the flag set that decides how much. Writing it
    /// re-cooks the mesh, so it belongs to a quality setting rather than to a per-frame value.
    /// </remarks>
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
