using UnityEngine;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("MVVM/Binders/UI/Canvas Group/CanvasGroup Binder - BlocksRaycasts EnumGroup")]
    public sealed class CanvasGroupBlocksRaycastsEnumGroupMonoBinder : EnumGroupMonoBinder<CanvasGroup>
    {
        [Header("Parameters")]
        [SerializeField] private bool _defaultValue;
        [SerializeField] private bool _selectedValue;
        
        protected override void SetDefaultValue(CanvasGroup element) =>
            element.blocksRaycasts = _defaultValue;

        protected override void SetSelectedValue(CanvasGroup element) =>
            element.blocksRaycasts = _selectedValue;
    }
}