using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{Image, Sprite}"/> that sets the <see cref="Image.sprite"/> property
    /// on each element based on the bound enum ViewModel value.
    /// Optionally disables each <see cref="Image"/> when its bound sprite is <see langword="null"/>.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Image/Image Binder – Sprite EnumGroup")]
    [AddBinderContextMenu(typeof(Image), serializePropertyNames: "m_Sprite", SubPath = "EnumGroup")]
    public sealed class ImageSpriteEnumGroupMonoBinder : EnumGroupMonoBinder<Image, Sprite>
    {
        [Tooltip("When enabled, disables each Image component when its bound sprite is null.")]
        [SerializeField] private bool _disabledWhenNull = true;
        
        /// <summary>
        /// Called when the bound enum resolves to a value for the specified element.
        /// Sets <see cref="Image.sprite"/> and disables the element when <paramref name="value"/> is <see langword="null"/> and <c>_disabledWhenNull</c> is <see langword="true"/>.
        /// </summary>
        protected override void SetValue(Image element, Sprite value)
        {
            element.sprite = value;
            element.enabled = !_disabledWhenNull || value;
        }
    }
}