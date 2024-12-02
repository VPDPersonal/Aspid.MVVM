using UnityEngine;
using UnityEngine.UI;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("UI/Binders/Image/Image Binder - Sprite Switcher")]
    public sealed class ImageSpriteSwitcherMonoBinder : SwitcherMonoBinder<Image, Sprite>
    {
        [SerializeField] private bool _disabledWhenNull = true;
        
        protected override void SetValue(Sprite value)
        {
            CachedComponent.sprite = value;
            if (_disabledWhenNull) CachedComponent.enabled = value != null;
        }
    }
}