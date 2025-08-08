using UnityEngine;
using Aspid.MVVM.Unity;

namespace Aspid.MVVM.StarterKit.Unity
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