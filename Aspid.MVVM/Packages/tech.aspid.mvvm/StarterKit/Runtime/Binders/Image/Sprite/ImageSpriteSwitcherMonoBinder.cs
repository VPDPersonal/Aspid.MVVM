using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherMonoBinder{TComponent, T}"/> that switches <see cref="Image.sprite"/>.
    /// </summary>
    /// <remarks>
    /// Optionally disables the <see cref="Image"/> while the sprite is <see langword="null"/>.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(Image), serializePropertyNames: "m_Sprite", SubPath = "Switcher")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Image/Image Binder – Sprite Switcher")]
    public sealed class ImageSpriteSwitcherMonoBinder : SwitcherMonoBinder<Image, Sprite>
    {
        [Tooltip("Disable the Image while the sprite is null.")]
        [SerializeField] private bool _disabledWhenNull = true;

        /// <inheritdoc/>
        protected override void SetValue(Sprite value) =>
            CachedComponent.SetSprite(value, _disabledWhenNull);
    }
}
