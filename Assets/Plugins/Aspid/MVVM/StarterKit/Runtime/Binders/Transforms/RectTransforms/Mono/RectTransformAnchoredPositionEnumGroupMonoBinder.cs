using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("MVVM/Binders/UI/RectTransform/RectTransform Binder - AnchoredPosition EnumGroup")]
    public sealed class RectTransformAnchoredPositionEnumGroupMonoBinder : EnumGroupMonoBinder<RectTransform>
    {
        [Header("Parameter")]
        [SerializeField] private Vector3 _defaultValue;
        [SerializeField] private Vector3 _selectedValue;
        
        [SerializeField] private Space _space = Space.World;

        [Header("Converters")]
        [SerializeField] private Vector3CombineConverter _defaultValueConverter = Vector3CombineConverter.Default;
        [SerializeField] private Vector3CombineConverter _selectedValueConverter = Vector3CombineConverter.Default;
        
        protected override void SetDefaultValue(RectTransform element) =>
            element.SetAnchoredPosition(_defaultValue, _space, _defaultValueConverter);

        protected override void SetSelectedValue(RectTransform element) =>
            element.SetAnchoredPosition(_selectedValue, _space, _selectedValueConverter);
    }
}