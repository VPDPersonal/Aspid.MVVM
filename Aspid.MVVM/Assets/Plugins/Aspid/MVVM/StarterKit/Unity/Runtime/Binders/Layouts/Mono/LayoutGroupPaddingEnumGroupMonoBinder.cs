using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(LayoutGroup), serializePropertyNames: "m_Padding")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Layout/LayoutGroup Binder – Padding EnumGroup")]
    public sealed class LayoutGroupPaddingEnumGroupMonoBinder : EnumGroupMonoBinder<LayoutGroup>
    {
        [SerializeField] private RectOffset _defaultValue;
        [SerializeField] private RectOffset _selectedValue;
        
        [SerializeField] private PaddingMode _paddingMode;

        protected override void SetDefaultValue(LayoutGroup element) =>
            SetValue(element, _defaultValue);

        protected override void SetSelectedValue(LayoutGroup element) =>
            SetValue(element, _selectedValue);
        
        private void SetValue(LayoutGroup element, RectOffset value) =>
            element.SetPadding(value.top, value.right, value.bottom, value.left, _paddingMode);
    }
}