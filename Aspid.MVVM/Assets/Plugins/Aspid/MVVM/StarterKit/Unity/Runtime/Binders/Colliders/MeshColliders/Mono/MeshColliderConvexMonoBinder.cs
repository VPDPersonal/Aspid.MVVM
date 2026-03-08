using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// MonoBehaviour binder that sets the <see cref="MeshCollider.convex"/> property on a <see cref="MeshCollider"/>
    /// when the bound ViewModel value changes.
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established the current value
    /// is sent back to the ViewModel. Supports optional value inversion.
    /// </summary>
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