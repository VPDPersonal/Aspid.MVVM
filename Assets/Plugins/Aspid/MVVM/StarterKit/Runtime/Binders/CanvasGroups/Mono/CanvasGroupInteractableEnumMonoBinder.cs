using UnityEngine;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Binders/UI/Canvas Group/CanvasGroup Binder - Interactable Enum")]
    public sealed class CanvasGroupInteractableEnumMonoBinder : EnumComponentMonoBinder<CanvasGroup, bool>
    {
        protected override void SetValue(bool value) =>
            CachedComponent.interactable = value;
    }
}