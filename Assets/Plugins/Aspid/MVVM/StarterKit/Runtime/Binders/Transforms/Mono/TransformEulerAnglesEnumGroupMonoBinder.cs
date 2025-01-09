using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("MVVM/Binders/Transform/Transform Binder - EulerAngles EnumGroup")]
    public sealed class TransformEulerAnglesEnumGroupMonoBinder : EnumGroupMonoBinder<Transform>
    {
        [Header("Parameters")]
        [SerializeField] private Vector3 _defaultValue;
        [SerializeField] private Vector3 _selectedValue;
        
        [SerializeField] private Space _space = Space.World;

        [Header("Converters")]
        [SerializeField] private Vector3CombineConverter _defaultValueConverter = Vector3CombineConverter.Default;
        [SerializeField] private Vector3CombineConverter _selectedValueConverter = Vector3CombineConverter.Default;

        protected override void SetDefaultValue(Transform element) =>
            element.SetEulerAngles(_defaultValue, _space, _defaultValueConverter);

        protected override void SetSelectedValue(Transform element) =>
            element.SetEulerAngles(_selectedValue, _space, _selectedValueConverter);
    }
}