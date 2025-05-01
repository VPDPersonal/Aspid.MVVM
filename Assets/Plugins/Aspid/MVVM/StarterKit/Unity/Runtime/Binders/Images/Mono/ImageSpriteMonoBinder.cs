using UnityEngine;
using UnityEngine.UI;

namespace Aspid.MVVM.StarterKit.Unity
{
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Image/Image Binder - Sprite")]
    public partial class ImageSpriteMonoBinder : ComponentMonoBinder<Image>, IBinder<Sprite>, IBinder<Texture2D>
    {
        [Header("Parameters")]
        [SerializeField] private bool _disabledWhenNull = true;

        [BinderLog]
        public void SetValue(Sprite value)
        {
            CachedComponent.sprite = value;
            if (_disabledWhenNull) CachedComponent.enabled = value is not null;
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