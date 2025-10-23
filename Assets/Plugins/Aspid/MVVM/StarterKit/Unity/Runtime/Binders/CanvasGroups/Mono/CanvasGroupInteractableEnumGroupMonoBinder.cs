using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddPropertyContextMenu(typeof(CanvasGroup), "m_Interactable")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Canvas Group/CanvasGroup Binder - Interactable EnumGroup")]
    [AddComponentContextMenu(typeof(CanvasGroup),"Add CanvasGroup Binder/CanvasGroup Binder - Interactable EnumGroup")]
    public sealed class CanvasGroupInteractableEnumGroupMonoBinder : EnumGroupMonoBinder<CanvasGroup>
    {
        [Header("Values")]
        [SerializeField] private bool _defaultValue;
        [SerializeField] private bool _selectedValue;
        
        protected override void SetDefaultValue(CanvasGroup element) =>
            element.ignoreParentGroups = _defaultValue;

        protected override void SetSelectedValue(CanvasGroup element) =>
            element.ignoreParentGroups = _selectedValue;
    }
}