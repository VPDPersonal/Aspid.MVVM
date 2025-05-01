using UnityEngine;

namespace Aspid.MVVM.StarterKit.Unity
{
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Mesh/MeshCollider Binder - Convex")]
    public partial class MeshColliderConvexMonoBinder : ComponentMonoBinder<MeshCollider>, IBinder<bool>
    {
        [Header("Converter")]
        [SerializeField] private bool _isInvert;

        [BinderLog]
        public void SetValue(bool value) =>
            CachedComponent.convex = _isInvert ? !value : value;
    }
}