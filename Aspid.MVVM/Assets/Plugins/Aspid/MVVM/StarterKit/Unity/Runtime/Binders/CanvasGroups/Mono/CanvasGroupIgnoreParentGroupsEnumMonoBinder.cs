using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// MonoBehaviour binder that sets the <see cref="CanvasGroup.ignoreParentGroups"/> property on a <see cref="CanvasGroup"/>
    /// to a value resolved from an enum bound on the ViewModel.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/CanvasGroup/CanvasGroup Binder – IgnoreParentGroups Enum")]
    [AddBinderContextMenu(typeof(CanvasGroup), serializePropertyNames: "m_IgnoreParentGroups", SubPath = "Enum")]
    public sealed class CanvasGroupIgnoreParentGroupsEnumMonoBinder : EnumMonoBinder<CanvasGroup, bool>
    {
        protected override void SetValue(bool value) =>
            CachedComponent.ignoreParentGroups = value;
    }
}