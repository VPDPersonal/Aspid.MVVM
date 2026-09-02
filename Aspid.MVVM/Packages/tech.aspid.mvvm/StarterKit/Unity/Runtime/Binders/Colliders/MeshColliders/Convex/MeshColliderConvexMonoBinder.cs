using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent,TProperty}"/> that binds the <see cref="MeshCollider.convex"/> property.
    /// </summary>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(MeshCollider), serializePropertyNames: "m_Convex")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Mesh/MeshCollider Binder – Convex")]
    public class MeshColliderConvexMonoBinder : ComponentMonoBinder<MeshCollider, bool>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => CachedComponent.convex;
            set => CachedComponent.convex = value;
        }
    }
}