using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{TElement, TValue}"/> that sets <see cref="Image.sprite"/> on each element.
    /// </summary>
    /// <remarks>
    /// Optionally disables the <see cref="Image"/> while the sprite is <see langword="null"/>.
    /// </remarks>
    [AddBinderContextMenu(typeof(Image), serializePropertyNames: "m_Sprite", SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Image/Image Binder – Sprite EnumGroup")]
    public sealed class ImageSpriteEnumGroupMonoBinder : EnumGroupMonoBinder<Image, Sprite>
    {
        [Tooltip("Disable the Image while the sprite is null.")]
        [SerializeField] private bool _disabledWhenNull = true;

        /// <inheritdoc/>
        protected override void SetValue(Image element, Sprite value) =>
            element.SetSprite(value, _disabledWhenNull);
    }
}
