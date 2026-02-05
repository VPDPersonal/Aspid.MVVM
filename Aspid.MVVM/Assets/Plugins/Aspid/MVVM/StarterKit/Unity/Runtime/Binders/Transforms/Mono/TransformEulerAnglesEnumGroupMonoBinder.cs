using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(Transform), serializePropertyNames: "m_LocalRotation")]
    [AddComponentMenu("Aspid/MVVM/Binders/Transform/Transform Binder – EulerAngles EnumGroup")]
    public sealed class TransformEulerAnglesEnumGroupMonoBinder : EnumGroupMonoBinder<Transform>
    {
        [SerializeField] private Vector3 _defaultValue;
        [SerializeField] private Vector3 _selectedValue;
      
        [SerializeField] private Space _space = Space.World;
        
        [SerializeField] private Vector3CombineConverter _defaultValueConverter = Vector3CombineConverter.Default;
        [SerializeField] private Vector3CombineConverter _selectedValueConverter = Vector3CombineConverter.Default;

        protected override void SetDefaultValue(Transform element) =>
            element.SetEulerAngles(_defaultValue, _space, _defaultValueConverter);

        protected override void SetSelectedValue(Transform element) =>
            element.SetEulerAngles(_selectedValue, _space, _selectedValueConverter);
    }
}