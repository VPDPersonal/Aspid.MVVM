using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Image/Image Binder – Sprite")]
    [AddBinderContextMenu(typeof(Image), serializePropertyNames: "m_Sprite")]
    public partial class ImageSpriteMonoBinder : ComponentMonoBinder<Image>, IBinder<Sprite>, IBinder<Texture2D>
    {
        [SerializeField] private bool _disabledWhenNull = true;

        [BinderLog]
        public void SetValue(Sprite value)
        {
            CachedComponent.sprite = value;
            CachedComponent.enabled = !_disabledWhenNull || value;
        }

        [BinderLog]
        public void SetValue(Texture2D value)
        {
            var sprite = !value 
                ? null 
                : Sprite.Create(value, new Rect(0, 0, value.width, value.height), new Vector2(0.5f, 0.5f));
            
            SetValue(sprite);
        }
    }
}