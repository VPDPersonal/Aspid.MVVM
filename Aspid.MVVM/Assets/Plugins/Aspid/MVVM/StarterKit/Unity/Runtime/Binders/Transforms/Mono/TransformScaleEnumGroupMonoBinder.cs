using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/Transform/Transform Binder – Scale EnumGroup")]
    [AddBinderContextMenu(typeof(Transform), serializePropertyNames: "m_LocalScale", SubPath = "EnumGroup")]
    public sealed class TransformScaleEnumGroupMonoBinder : EnumGroupMonoBinder<Transform>
    {
        [SerializeField] private Vector3 _defaultValue;
        [SerializeField] private Vector3 _selectedValue;
        
        [SerializeField] private Vector3CombineConverter _converter = Vector3CombineConverter.Default;

        protected override void SetDefaultValue(Transform element) =>
            element.SetScale(_defaultValue, _converter);

        protected override void SetSelectedValue(Transform element) =>
            element.SetScale(_selectedValue, _converter);
    }
}