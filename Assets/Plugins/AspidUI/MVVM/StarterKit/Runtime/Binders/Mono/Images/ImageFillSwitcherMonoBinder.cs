using UnityEngine;
using UnityEngine.UI;

namespace AspidUI.MVVM.StarterKit.Binders.Mono.Images
{
    [AddComponentMenu("UI/Binders/Image/Image Binder - Fill Switcher")]
    public sealed class ImageFillSwitcherMonoBinder : SwitcherMonoBinder<Image, float>
    {
        protected override void SetValue(float value) =>
            CachedComponent.fillAmount = value;
    }
}