using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Sphere/SphereCollider Binder - Center EnumGroup")]
    public sealed class SphereColliderCentreEnumGroupMonoBinder : EnumGroupMonoBinder<SphereCollider>
    {
        [Header("Parameters")]
        [SerializeField] private Vector3 _defaultValue;
        [SerializeField] private Vector3 _selectedValue;
        
        [Header("Converters")]
        [SerializeField] private Vector3CombineConverter _defaultValueConverter = Vector3CombineConverter.Default;
        [SerializeField] private Vector3CombineConverter _selectedValueConverter = Vector3CombineConverter.Default;
        
        protected override void SetDefaultValue(SphereCollider element) =>
            element.center = _defaultValueConverter.Convert(_defaultValue, element.center);

        protected override void SetSelectedValue(SphereCollider element) =>
            element.center = _selectedValueConverter.Convert(_defaultValue, element.center);
    }
}