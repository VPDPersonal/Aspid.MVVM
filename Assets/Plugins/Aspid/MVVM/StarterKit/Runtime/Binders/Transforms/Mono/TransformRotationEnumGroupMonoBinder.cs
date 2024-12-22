using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Binders/Transform/Transform Binder - Rotation EnumGroup")]
    public sealed class TransformRotationEnumGroupMonoBinder : EnumGroupMonoBinder<Transform>
    {
        [Header("Parameters")]
        [SerializeField] private Vector3 _defaultValue;
        [SerializeField] private Vector3 _selectedValue;
        [SerializeField] private Space _space = Space.World;

        [Header("Converters")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#if UNITY_2023_1_OR_NEWER
        private IConverter<Quaternion, Quaternion> _defaultValueConverter;
#else
        private IConverterQuaternion _defaultValueConverter;
#endif
        
        [SerializeReference]
        [SerializeReferenceDropdown]
#if UNITY_2023_1_OR_NEWER
        private IConverter<Quaternion, Quaternion> _selectedValueConverter;
#else
        private IConverterQuaternion _selectedValueConverter;
#endif
        
        protected override void SetDefaultValue(Transform element)
        {
            var rotation = Quaternion.Euler(_defaultValue);
            rotation = _defaultValueConverter?.Convert(rotation) ?? rotation;
            element.SetRotation(rotation, _space);
        }

        protected override void SetSelectedValue(Transform element)
        {
            var rotation = Quaternion.Euler(_selectedValue);
            rotation = _selectedValueConverter?.Convert(rotation) ?? rotation;
            element.SetRotation(rotation, _space);
        }
    }
}