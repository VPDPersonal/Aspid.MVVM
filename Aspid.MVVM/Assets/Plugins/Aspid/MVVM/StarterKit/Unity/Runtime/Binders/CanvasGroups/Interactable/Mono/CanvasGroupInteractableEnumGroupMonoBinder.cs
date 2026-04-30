using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{CanvasGroup, bool}"/> that sets the <see cref="CanvasGroup.interactable"/>
    /// property on each <see cref="CanvasGroup"/> in the group based on the bound enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/CanvasGroup/CanvasGroup Binder – Interactable EnumGroup")]
    [AddBinderContextMenu(typeof(CanvasGroup), serializePropertyNames: "m_Interactable", SubPath = "EnumGroup")]
    public sealed class CanvasGroupInteractableEnumGroupMonoBinder : EnumGroupMonoBinder<CanvasGroup, bool>
    {
        /// <inheritdoc/>
        protected override void SetValue(CanvasGroup element, bool value) =>
            element.interactable = value;
    }
}