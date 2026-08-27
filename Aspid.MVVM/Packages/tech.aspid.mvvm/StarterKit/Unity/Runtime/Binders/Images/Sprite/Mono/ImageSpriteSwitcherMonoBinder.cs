using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherMonoBinder{Image, Sprite}"/> that switches the <see cref="Image.sprite"/> property
    /// between two <see cref="Sprite"/> assets based on the bound boolean ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Image/Image Binder – Sprite Switcher")]
    [AddBinderContextMenu(typeof(Image), serializePropertyNames: "m_Sprite", SubPath = "Switcher")]
    public sealed class ImageSpriteSwitcherMonoBinder : SwitcherMonoBinder<Image, Sprite>
    {
        [Tooltip("When enabled, disables the Image component when the selected sprite is null.")]
        [SerializeField] private bool _disabledWhenNull = true;
        
        /// <summary>
        /// Sets <see cref="Image.sprite"/> to <paramref name="value"/>; disables the <see cref="Image"/> when
        /// <paramref name="value"/> is <see langword="null"/> and disable-when-null is enabled.
        /// </summary>
        protected override void SetValue(Sprite value)
        {
            CachedComponent.sprite = value;
            if (_disabledWhenNull) CachedComponent.enabled = value;
        }
    }
}