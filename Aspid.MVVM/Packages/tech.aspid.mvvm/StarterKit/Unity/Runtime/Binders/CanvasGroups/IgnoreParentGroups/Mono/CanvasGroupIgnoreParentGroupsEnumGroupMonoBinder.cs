using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{CanvasGroup, bool}"/> that sets the <see cref="CanvasGroup.ignoreParentGroups"/>
    /// property on each <see cref="CanvasGroup"/> in the group based on the bound enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/CanvasGroup/CanvasGroup Binder – IgnoreParentGroups EnumGroup")]
    [AddBinderContextMenu(typeof(CanvasGroup), serializePropertyNames: "m_IgnoreParentGroups", SubPath = "EnumGroup")]
    public sealed class CanvasGroupIgnoreParentGroupsEnumGroupMonoBinder : EnumGroupMonoBinder<CanvasGroup, bool>
    {
        /// <inheritdoc/>
        protected override void SetValue(CanvasGroup element, bool value) =>
            element.ignoreParentGroups = value;
    }
}