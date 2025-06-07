using UnityEngine;
using Aspid.MVVM.Unity;

namespace Aspid.MVVM.StarterKit.Unity
{
    [AddPropertyContextMenu(typeof(MeshCollider), "m_Convex")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Mesh/MeshCollider Binder - Convex")]
    [AddComponentContextMenu(typeof(MeshCollider),"Add MeshCollider Binder/MeshCollider Binder - Convex")]
    public partial class MeshColliderConvexMonoBinder : ComponentMonoBinder<MeshCollider>, IBinder<bool>
    {
        [Header("Converter")]
        [SerializeField] private bool _isInvert;

        [BinderLog]
        public void SetValue(bool value) =>
            CachedComponent.convex = _isInvert ? !value : value;
    }
}