using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<float, float>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterFloat;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Capsule/CapsuleCollider Binder – Radius EnumGroup")]
    [AddBinderContextMenu(typeof(CapsuleCollider), serializePropertyNames: "m_Radius", SubPath = "EnumGroup")]
    public sealed class CapsuleColliderRadiusEnumGroupMonoBinder : EnumGroupMonoBinder<CapsuleCollider>
    {
        [SerializeField] private float _defaultValue;
        [SerializeField] private float _selectedValue;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _defaultValueConverter;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _selectedValueConverter;
        
        protected override void SetDefaultValue(CapsuleCollider element) =>
            element.radius = _defaultValueConverter?.Convert(_defaultValue) ?? _defaultValue;

        protected override void SetSelectedValue(CapsuleCollider element) =>
            element.radius =  _selectedValueConverter?.Convert(_selectedValue) ?? _selectedValue;
    }
}