using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
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