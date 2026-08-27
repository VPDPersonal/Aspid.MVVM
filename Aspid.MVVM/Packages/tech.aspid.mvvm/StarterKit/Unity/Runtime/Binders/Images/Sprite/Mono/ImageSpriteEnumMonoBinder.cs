using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{Image, Sprite}"/> that sets the <see cref="Image.sprite"/> property
    /// based on the bound enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Image/Image Binder – Sprite Enum")]
    [AddBinderContextMenu(typeof(Image), serializePropertyNames: "m_Sprite", SubPath = "Enum")]
    public sealed class ImageSpriteEnumMonoBinder : EnumMonoBinder<Image, Sprite>
    {
        [Tooltip("When enabled, disables the Image component when the bound sprite is null.")]
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