using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Image/Image Binder – Sprite Switcher")]
    [AddBinderContextMenu(typeof(Image), serializePropertyNames: "m_Sprite", SubPath = "Switcher")]
    public sealed class ImageSpriteSwitcherMonoBinder : SwitcherMonoBinder<Image, Sprite>
    {
        [SerializeField] private bool _disabledWhenNull = true;
        
        protected override void SetValue(Sprite value)
        {
            CachedComponent.sprite = value;
            CachedComponent.enabled = !_disabledWhenNull || value;
        }
    }
}