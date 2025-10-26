using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Quaternion, UnityEngine.Quaternion>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterQuaternion;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddPropertyContextMenu(typeof(Transform), "m_LocalRotation")]
    [AddComponentMenu("Aspid/MVVM/Binders/Transform/Transform Binder - Rotation EnumGroup")]
    [AddComponentContextMenu(typeof(Transform),"Add Transform Binder/Transform Binder - Rotation EnumGroup")]
    public sealed class TransformRotationEnumGroupMonoBinder : EnumGroupMonoBinder<Transform>
    {
        [Header("Values")]
        [SerializeField] private Vector3 _defaultValue;
        [SerializeField] private Vector3 _selectedValue;
        [SerializeField] private Space _space = Space.World;

        [Header("Converters")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _defaultValueConverter;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _selectedValueConverter;
        
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