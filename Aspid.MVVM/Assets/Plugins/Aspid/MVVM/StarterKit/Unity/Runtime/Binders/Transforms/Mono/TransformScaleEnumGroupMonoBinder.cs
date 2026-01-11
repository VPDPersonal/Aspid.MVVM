using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(Transform), serializePropertyNames: "m_LocalScale")]
    [AddComponentMenu("Aspid/MVVM/Binders/Transform/Transform Binder – Scale EnumGroup")]
    public sealed class TransformScaleEnumGroupMonoBinder : EnumGroupMonoBinder<Transform>
    {
        [Header("Values")]
        [SerializeField] private Vector3 _defaultValue;
        [SerializeField] private Vector3 _selectedValue;
        
        [Header("Converters")]
        [SerializeField] private Vector3CombineConverter _converter = Vector3CombineConverter.Default;

        protected override void SetDefaultValue(Transform element) =>
            element.SetScale(_defaultValue, _converter);

        protected override void SetSelectedValue(Transform element) =>
            element.SetScale(_selectedValue, _converter);
    }
}