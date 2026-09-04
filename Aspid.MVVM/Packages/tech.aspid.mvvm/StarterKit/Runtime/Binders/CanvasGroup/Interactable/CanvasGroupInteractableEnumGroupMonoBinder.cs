using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{TElement, TValue}"/> that sets
    /// <see cref="CanvasGroup.interactable"/> on each element.
    /// </summary>
    [AddBinderContextMenu(typeof(CanvasGroup), serializePropertyNames: "m_Interactable", SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/CanvasGroup/CanvasGroup Binder – Interactable EnumGroup")]
    public sealed class CanvasGroupInteractableEnumGroupMonoBinder : EnumGroupMonoBinder<CanvasGroup, bool>
    {
        /// <inheritdoc/>
        protected override void SetValue(CanvasGroup element, bool value) =>
            element.interactable = value;
    }
}
