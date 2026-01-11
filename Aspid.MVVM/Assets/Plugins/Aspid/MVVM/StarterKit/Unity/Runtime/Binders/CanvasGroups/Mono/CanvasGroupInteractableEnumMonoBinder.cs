using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(CanvasGroup), serializePropertyNames: "m_Interactable")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Canvas Group/CanvasGroup Binder – Interactable Enum")]
    public sealed class CanvasGroupInteractableEnumMonoBinder : EnumMonoBinder<CanvasGroup, bool>
    {
        protected override void SetValue(bool value) =>
            CachedComponent.interactable = value;
    }
}