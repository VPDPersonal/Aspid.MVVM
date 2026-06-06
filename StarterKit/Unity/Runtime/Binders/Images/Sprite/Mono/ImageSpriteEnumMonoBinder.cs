using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{Image, Sprite}"/> that sets the <see cref="Image.sprite"/> property
    /// based on the bound enum ViewModel value.
    /// Optionally disables the <see cref="Image"/> when the bound sprite is <see langword="null"/>.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Image/Image Binder – Sprite Enum")]
    [AddBinderContextMenu(typeof(Image), serializePropertyNames: "m_Sprite", SubPath = "Enum")]
    public sealed class ImageSpriteEnumMonoBinder : EnumMonoBinder<Image, Sprite>
    {
        [Tooltip("When enabled, disables the Image component when the bound sprite is null.")]
        [SerializeField] private bool _disabledWhenNull = true;
        
        /// <summary>
        /// Called when the bound enum resolves to a value for the current element.
        /// Sets <see cref="Image.sprite"/> and disables the <see cref="Image"/> when <paramref name="value"/> is <see langword="null"/> and the disable-when-null option is enabled.
        /// </summary>
        protected override void SetValue(Sprite value)
        {
            CachedComponent.sprite = value;
            CachedComponent.enabled = !_disabledWhenNull || value;
        }
    }
}