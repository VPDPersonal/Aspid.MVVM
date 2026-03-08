using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// MonoBehaviour binder that sets the <see cref="CanvasGroup.ignoreParentGroups"/> property on a group of <see cref="CanvasGroup"/>
    /// components, applying the configured selected or default value to each entry based on the bound
    /// enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/CanvasGroup/CanvasGroup Binder – IgnoreParentGroups EnumGroup")]
    [AddBinderContextMenu(typeof(CanvasGroup), serializePropertyNames: "m_IgnoreParentGroups", SubPath = "EnumGroup")]
    public sealed class CanvasGroupIgnoreParentGroupsEnumGroupMonoBinder : EnumGroupMonoBinder<CanvasGroup, bool>
    {
        protected override void SetValue(CanvasGroup element, bool value) =>
            element.ignoreParentGroups = value;
    }
}