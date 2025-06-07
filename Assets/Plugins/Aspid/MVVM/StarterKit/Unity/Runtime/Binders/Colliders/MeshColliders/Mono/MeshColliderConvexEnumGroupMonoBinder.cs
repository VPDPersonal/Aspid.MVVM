using UnityEngine;
using Aspid.MVVM.Unity;

namespace Aspid.MVVM.StarterKit.Unity
{
    [AddPropertyContextMenu(typeof(MeshCollider), "m_Convex")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Mesh/MeshCollider Binder - Convex EnumGroup")]
    [AddComponentContextMenu(typeof(MeshCollider),"Add MeshCollider Binder/MeshCollider Binder - Convex EnumGroup")]
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