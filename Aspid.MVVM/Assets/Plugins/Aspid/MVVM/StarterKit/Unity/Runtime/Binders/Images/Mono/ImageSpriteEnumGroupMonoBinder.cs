using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// MonoBehaviour binder that sets the <see cref="Image.sprite"/> property on a group of <see cref="Image"/>
    /// components, applying the configured selected or default value to each entry based on the bound
    /// enum ViewModel value. Optionally disables each <see cref="Image"/> when its bound sprite is <see langword="null"/>.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Image/Image Binder – Sprite EnumGroup")]
    [AddBinderContextMenu(typeof(Image), serializePropertyNames: "m_Sprite", SubPath = "EnumGroup")]
    public sealed class ImageSpriteEnumGroupMonoBinder : EnumGroupMonoBinder<Image, Sprite>
    {
        [SerializeField] private bool _disabledWhenNull = true;
        
        protected override void SetValue(Image element, Sprite value)
        {
            element.sprite = value;
            element.enabled = !_disabledWhenNull || value;
        }
    }
}