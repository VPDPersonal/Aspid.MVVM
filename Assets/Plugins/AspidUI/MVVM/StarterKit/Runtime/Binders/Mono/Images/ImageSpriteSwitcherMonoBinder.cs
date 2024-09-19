using UnityEngine;
using UnityEngine.UI;

namespace AspidUI.MVVM.StarterKit.Binders.Mono.Images
{
    [AddComponentMenu("UI/Binders/Image/Image Binder - Sprite Switcher")]
    public sealed class ImageSpriteSwitcherMonoBinder : SwitcherMonoBinder<Image, Sprite>
    {
        protected override void SetValue(Sprite value) =>
            CachedComponent.sprite = value;
    }
}