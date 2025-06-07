using UnityEngine;
using Aspid.MVVM.Unity;

namespace Aspid.MVVM.StarterKit.Unity
{
    [AddPropertyContextMenu(typeof(CanvasGroup), "m_IgnoreParentGroups")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Canvas Group/CanvasGroup Binder - IgnoreParentGroups EnumGroup")]
    [AddComponentContextMenu(typeof(CanvasGroup),"Add CanvasGroup Binder/CanvasGroup Binder - IgnoreParentGroups EnumGroup")]
    public sealed class CanvasGroupIgnoreParentGroupsEnumGroupMonoBinder : EnumGroupMonoBinder<CanvasGroup>
    {
        [Header("Parameters")]
        [SerializeField] private bool _defaultValue;
        [SerializeField] private bool _selectedValue;
        
        protected override void SetDefaultValue(CanvasGroup element) =>
            element.ignoreParentGroups = _defaultValue;

        protected override void SetSelectedValue(CanvasGroup element) =>
            element.ignoreParentGroups = _selectedValue;
    }
}