using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherMonoBinder{Image, Sprite}"/> that switches the <see cref="Image.sprite"/> property
    /// between two <see cref="Sprite"/> assets based on the bound boolean ViewModel value.
    /// Optionally disables the <see cref="Image"/> when the selected sprite is <see langword="null"/>.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Image/Image Binder – Sprite Switcher")]
    [AddBinderContextMenu(typeof(Image), serializePropertyNames: "m_Sprite", SubPath = "Switcher")]
    public sealed class ImageSpriteSwitcherMonoBinder : SwitcherMonoBinder<Image, Sprite>
    {
        [Tooltip("When enabled, disables the Image component when the selected sprite is null.")]
        [SerializeField] private bool _disabledWhenNull = true;
        
        /// <summary>
        /// Called when applying the selected value to the <see cref="Image.sprite"/> property.
        /// Disables the <see cref="Image"/> when <paramref name="value"/> is <see langword="null"/> and the disable-when-null option is enabled.
        /// </summary>
        protected override void SetValue(Sprite value)
        {
            CachedComponent.sprite = value;
            CachedComponent.enabled = !_disabledWhenNull || value;
        }
    }
}