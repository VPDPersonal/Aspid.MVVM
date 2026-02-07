using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Sphere/SphereCollider Binder – Center EnumGroup")]
    [AddBinderContextMenu(typeof(SphereCollider), serializePropertyNames: "m_Center", SubPath = "EnumGroup")]
    public sealed class SphereColliderCenterEnumGroupMonoBinder : EnumGroupMonoBinder<SphereCollider>
    {
        [SerializeField] private Vector3 _defaultValue;
        [SerializeField] private Vector3 _selectedValue;
        
        [SerializeField] private Vector3CombineConverter _defaultValueConverter = Vector3CombineConverter.Default;
        [SerializeField] private Vector3CombineConverter _selectedValueConverter = Vector3CombineConverter.Default;
        
        protected override void SetDefaultValue(SphereCollider element) =>
            element.center = _defaultValueConverter.Convert(_defaultValue, element.center);

        protected override void SetSelectedValue(SphereCollider element) =>
            element.center = _selectedValueConverter.Convert(_defaultValue, element.center);
    }
}