using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{Image, Sprite}"/> that sets the <see cref="Image.sprite"/> property
    /// on each element based on the bound enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Image/Image Binder – Sprite EnumGroup")]
    [AddBinderContextMenu(typeof(Image), serializePropertyNames: "m_Sprite", SubPath = "EnumGroup")]
    public sealed class ImageSpriteEnumGroupMonoBinder : EnumGroupMonoBinder<Image, Sprite>
    {
        [Tooltip("When enabled, disables each Image component when its bound sprite is null.")]
        [SerializeField] private bool _disabledWhenNull = true;
        
        /// <summary>
        /// Sets <see cref="Image.sprite"/> on <paramref name="element"/> to <paramref name="value"/>; disables it when
        /// <paramref name="value"/> is <see langword="null"/> and disable-when-null is enabled.
        /// </summary>
        protected override void SetValue(Image element, Sprite value)
        {
            element.sprite = value;
            if (_disabledWhenNull) element.enabled = value;
        }
    }
}