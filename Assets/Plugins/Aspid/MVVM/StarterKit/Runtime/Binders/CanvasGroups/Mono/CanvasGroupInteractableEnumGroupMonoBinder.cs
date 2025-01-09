using UnityEngine;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("MVVM/Binders/UI/Canvas Group/CanvasGroup Binder - Interactable EnumGroup")]
    public sealed class CanvasGroupInteractableEnumGroupMonoBinder : EnumGroupMonoBinder<CanvasGroup>
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