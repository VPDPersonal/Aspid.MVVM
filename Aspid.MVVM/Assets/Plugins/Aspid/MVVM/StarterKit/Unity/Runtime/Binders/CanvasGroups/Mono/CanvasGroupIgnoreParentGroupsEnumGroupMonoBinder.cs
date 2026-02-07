using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Canvas Group/CanvasGroup Binder – IgnoreParentGroups EnumGroup")]
    [AddBinderContextMenu(typeof(CanvasGroup), serializePropertyNames: "m_IgnoreParentGroups", SubPath = "EnumGroup")]
    public sealed class CanvasGroupIgnoreParentGroupsEnumGroupMonoBinder : EnumGroupMonoBinder<CanvasGroup, bool>
    {
        protected override void SetValue(CanvasGroup element, bool value) =>
            element.ignoreParentGroups = value;
    }
}