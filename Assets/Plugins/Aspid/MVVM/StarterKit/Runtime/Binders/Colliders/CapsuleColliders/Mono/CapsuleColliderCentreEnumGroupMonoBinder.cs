using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Binders/Collider/CapsuleCollider Binder - Center EnumGroup")]
    public sealed class CapsuleColliderCentreEnumGroupMonoBinder : EnumGroupMonoBinder<CapsuleCollider>
    {
        [Header("Parameters")]
        [SerializeField] private Vector3 _defaultValue;
        [SerializeField] private Vector3 _selectedValue;
        
        [Header("Converters")]
        [SerializeField] private Vector3CombineConverter _defaultValueConverter = Vector3CombineConverter.Default;
        [SerializeField] private Vector3CombineConverter _selectedValueConverter = Vector3CombineConverter.Default;
        
        protected override void SetDefaultValue(CapsuleCollider element) =>
            element.center = _defaultValueConverter.Convert(_defaultValue, element.center);

        protected override void SetSelectedValue(CapsuleCollider element) =>
            element.center = _selectedValueConverter.Convert(_defaultValue, element.center);
    }
}