using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// MonoBehaviour binder that sets the sprite of the host <see cref="Image"/> component
    /// by mapping an enum-indexed source value to a <see cref="Sprite"/>.
    /// Optionally disables the image when the bound sprite is <see langword="null"/>.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Image/Image Binder – Sprite Enum")]
    [AddBinderContextMenu(typeof(Image), serializePropertyNames: "m_Sprite", SubPath = "Enum")]
    public sealed class ImageSpriteEnumMonoBinder : EnumMonoBinder<Image, Sprite>
    {
        [SerializeField] private bool _disabledWhenNull = true;
        
        protected override void SetValue(Sprite value)
        {
            CachedComponent.sprite = value;
            CachedComponent.enabled = !_disabledWhenNull || value;
        }
    }
}