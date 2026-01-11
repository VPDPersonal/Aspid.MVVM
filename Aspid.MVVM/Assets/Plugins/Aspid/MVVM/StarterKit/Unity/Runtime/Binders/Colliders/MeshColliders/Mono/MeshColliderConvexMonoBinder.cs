using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(MeshCollider), serializePropertyNames: "m_Convex")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Mesh/MeshCollider Binder – Convex")]
    public partial class MeshColliderConvexMonoBinder : ComponentMonoBinder<MeshCollider>, IBinder<bool>
    {
        [SerializeField] private bool _isInvert;

        [BinderLog]
        public void SetValue(bool value) =>
            CachedComponent.convex = _isInvert ? !value : value;
    }
}