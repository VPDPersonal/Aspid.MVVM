using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// MonoBehaviour binder that sets the <see cref="CanvasGroup.interactable"/> property on a <see cref="CanvasGroup"/>
    /// to a value resolved from an enum bound on the ViewModel.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/CanvasGroup/CanvasGroup Binder – Interactable Enum")]
    [AddBinderContextMenu(typeof(CanvasGroup), serializePropertyNames: "m_Interactable", SubPath = "Enum")]
    public sealed class CanvasGroupInteractableEnumMonoBinder : EnumMonoBinder<CanvasGroup, bool>
    {
        protected override void SetValue(bool value) =>
            CachedComponent.interactable = value;
    }
}