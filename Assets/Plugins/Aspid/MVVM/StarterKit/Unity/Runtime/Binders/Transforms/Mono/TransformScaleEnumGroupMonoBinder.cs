using UnityEngine;
using Aspid.MVVM.Unity;

namespace Aspid.MVVM.StarterKit.Unity
{
    [AddPropertyContextMenu(typeof(Transform), "m_LocalScale")]
    [AddComponentMenu("Aspid/MVVM/Binders/Transform/Transform Binder - Scale EnumGroup")]
    [AddComponentContextMenu(typeof(Transform),"Add Transform Binder/Transform Binder - Scale EnumGroup")]
    public sealed class TransformScaleEnumGroupMonoBinder : EnumGroupMonoBinder<Transform>
    {
        [Header("Parameters")]
        [SerializeField] private Vector3 _defaultValue;
        [SerializeField] private Vector3 _selectedValue;
        
        [Header("Converter")]
        [SerializeField] private Vector3CombineConverter _converter = Vector3CombineConverter.Default;

        protected override void SetDefaultValue(Transform element) =>
            element.SetScale(_defaultValue, _converter);

        protected override void SetSelectedValue(Transform element) =>
            element.SetScale(_selectedValue, _converter);
    }
}