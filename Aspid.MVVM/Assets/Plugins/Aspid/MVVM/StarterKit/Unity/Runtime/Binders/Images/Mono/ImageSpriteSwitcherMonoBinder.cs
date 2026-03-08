using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// MonoBehaviour binder that sets the sprite of the host <see cref="Image"/> component,
    /// toggling between two pre-configured <see cref="Sprite"/> assets based on a bound boolean value.
    /// Optionally disables the image when the selected sprite is <see langword="null"/>.
    /// </summary>
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