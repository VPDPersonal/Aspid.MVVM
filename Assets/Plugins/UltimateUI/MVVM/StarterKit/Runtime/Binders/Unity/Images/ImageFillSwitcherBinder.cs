using UnityEngine;
using UnityEngine.UI;

namespace UltimateUI.MVVM.StarterKit.Binders.Unity.Images
{
    [AddComponentMenu("UI/Binders/Image/Image Binder - Fill Switcher")]
    public sealed class ImageFillSwitcherBinder : SwitcherMonoBinder<Image, float>
    {
        protected override void SetValue(float value) =>
            CachedComponent.fillAmount = value;
    }
}