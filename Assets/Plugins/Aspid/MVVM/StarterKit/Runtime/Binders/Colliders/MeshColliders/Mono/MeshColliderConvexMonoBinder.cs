using UnityEngine;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("MVVM/Binders/Collider/Mesh/MeshCollider Binder - Convex")]
    public class MeshColliderConvexMonoBinder : ComponentMonoBinder<MeshCollider>, IBinder<bool>
    {
        [Header("Converter")]
        [SerializeField] private bool _isInvert;

        public void SetValue(bool value) =>
            CachedComponent.convex = _isInvert ? !value : value;
    }
}