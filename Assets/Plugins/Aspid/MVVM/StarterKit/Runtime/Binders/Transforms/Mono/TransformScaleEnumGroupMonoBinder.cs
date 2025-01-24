using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Aspid/MVVM/Binders/Transform/Transform Binder - Scale EnumGroup")]
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