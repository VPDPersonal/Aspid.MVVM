using UnityEngine;
using Aspid.MVVM.Unity;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<float, float>;
#else
using Converter = Aspid.MVVM.StarterKit.Unity.IConverterFloat;
#endif

namespace Aspid.MVVM.StarterKit.Unity
{
    [AddPropertyContextMenu(typeof(SphereCollider), "m_Radius")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Sphere/SphereCollider Binder - Radius EnumGroup")]
    [AddComponentContextMenu(typeof(SphereCollider),"Add SphereCollider Binder/SphereCollider Binder - Radius EnumGroup")]
    public sealed class SphereColliderRadiusEnumGroupMonoBinder : EnumGroupMonoBinder<SphereCollider>
    {
        [Header("Parameters")]
        [SerializeField] [Min(0)] private float _defaultValue;
        [SerializeField] [Min(0)] private float _selectedValue;
        
        [Header("Converters")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _defaultValueConverter;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _selectedValueConverter;
        
        protected override void SetDefaultValue(SphereCollider element) =>
            element.radius = _defaultValueConverter?.Convert(_defaultValue) ?? _defaultValue;

        protected override void SetSelectedValue(SphereCollider element) =>
            element.radius = _selectedValueConverter?.Convert(_selectedValue) ?? _selectedValue;
    }
}