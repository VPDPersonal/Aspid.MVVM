using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(MeshCollider), serializePropertyNames: "m_Convex")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Mesh/MeshCollider Binder – Convex")]
    public class MeshColliderConvexMonoBinder : ComponentBoolMonoBinder<MeshCollider>
    {
        protected sealed override bool Property
        {
            get => CachedComponent.convex;
            set => CachedComponent.convex = value;
        }
    }
}