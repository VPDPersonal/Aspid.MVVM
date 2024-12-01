using UnityEngine;
using UnityEngine.UI;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("UI/Binders/Image/Image Binder - Fill Switcher")]
    public sealed class ImageFillSwitcherMonoBinder : SwitcherMonoBinder<Image, float>
    {
        protected override void SetValue(float value) =>
            CachedComponent.fillAmount = value;
    }
}