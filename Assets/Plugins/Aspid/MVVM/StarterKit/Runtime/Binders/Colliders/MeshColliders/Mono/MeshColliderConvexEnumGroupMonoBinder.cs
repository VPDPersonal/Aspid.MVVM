using UnityEngine;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Binders/Collider/MeshCollider Binder - Convex EnumGroup")]
    public sealed class MeshColliderConvexEnumGroupMonoBinder : EnumGroupMonoBinder<MeshCollider>
    {
        [Header("Parameters")]
        [SerializeField] private bool _defaultValue;
        [SerializeField] private bool _selectedValue;

        protected override void SetDefaultValue(MeshCollider element) =>
            element.convex = _defaultValue;

        protected override void SetSelectedValue(MeshCollider element) =>
            element.convex = _selectedValue;
    }
}