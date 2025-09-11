using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddPropertyContextMenu(typeof(CanvasGroup), "m_BlocksRaycasts")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Canvas Group/CanvasGroup Binder - BlocksRaycasts EnumGroup")]
    [AddComponentContextMenu(typeof(CanvasGroup),"Add CanvasGroup Binder/CanvasGroup Binder - BlocksRaycasts EnumGroup")]
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