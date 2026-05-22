using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentBoolMonoBinder{MeshCollider}"/> that binds the <see cref="MeshCollider.convex"/> property.
    /// </summary>
    /// <remarks>
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established, the current convex value
    /// is sent back to the ViewModel.
    /// </remarks>
    [AddBinderContextMenu(typeof(MeshCollider), serializePropertyNames: "m_Convex")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Mesh/MeshCollider Binder – Convex")]
    public class MeshColliderConvexMonoBinder : ComponentBoolMonoBinder<MeshCollider>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => CachedComponent.convex;
            set => CachedComponent.convex = value;
        }
    }
}