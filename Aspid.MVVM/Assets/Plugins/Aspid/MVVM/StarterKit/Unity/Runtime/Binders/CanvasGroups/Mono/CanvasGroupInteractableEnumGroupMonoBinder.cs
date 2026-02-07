using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Canvas Group/CanvasGroup Binder – Interactable EnumGroup")]
    [AddBinderContextMenu(typeof(CanvasGroup), serializePropertyNames: "m_Interactable", SubPath = "EnumGroup")]
    public sealed class CanvasGroupInteractableEnumGroupMonoBinder : EnumGroupMonoBinder<CanvasGroup, bool>
    {
        protected override void SetValue(CanvasGroup element, bool value) =>
            element.ignoreParentGroups = value;
    }
}