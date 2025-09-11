using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddPropertyContextMenu(typeof(CanvasGroup), "m_IgnoreParentGroups")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Canvas Group/CanvasGroup Binder - IgnoreParentGroups Enum")]
    [AddComponentContextMenu(typeof(CanvasGroup),"Add CanvasGroup Binder/CanvasGroup Binder - IgnoreParentGroups Enum")]
    public sealed class CanvasGroupIgnoreParentGroupsEnumMonoBinder : EnumMonoBinder<CanvasGroup, bool>
    {
        protected override void SetValue(bool value) =>
            CachedComponent.ignoreParentGroups = value;
    }
}