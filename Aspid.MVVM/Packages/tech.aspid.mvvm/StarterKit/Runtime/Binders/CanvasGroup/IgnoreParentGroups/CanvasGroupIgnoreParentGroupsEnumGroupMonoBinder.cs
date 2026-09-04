using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{TElement, TValue}"/> that sets
    /// <see cref="CanvasGroup.ignoreParentGroups"/> on each element.
    /// </summary>
    [AddBinderContextMenu(typeof(CanvasGroup), serializePropertyNames: "m_IgnoreParentGroups", SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/CanvasGroup/CanvasGroup Binder – Ignore Parent Groups EnumGroup")]
    public sealed class CanvasGroupIgnoreParentGroupsEnumGroupMonoBinder : EnumGroupMonoBinder<CanvasGroup, bool>
    {
        /// <inheritdoc/>
        protected override void SetValue(CanvasGroup element, bool value) =>
            element.ignoreParentGroups = value;
    }
}
