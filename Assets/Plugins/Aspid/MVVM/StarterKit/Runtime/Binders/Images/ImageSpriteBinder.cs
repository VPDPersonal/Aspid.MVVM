#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public sealed class ImageSpriteBinder : Binder, IBinder<Sprite?>, IBinder<Texture2D?>
    {
        [Header("Component")]
        [SerializeField] private Image _image;
        
        [Header("Parameter")]
        [SerializeField] private bool _disabledWhenNull;

        public ImageSpriteBinder(Image image, bool disabledWhenNull = true)
        {
            _disabledWhenNull = disabledWhenNull;
            _image = image ?? throw new ArgumentNullException(nameof(image));
        }

        public void SetValue(Sprite? value)
        {
            _image.sprite = value;
            if (_disabledWhenNull) _image.enabled = value is not null;
        }

        public void SetValue(Texture2D? value)
        {
            var sprite = !value 
                ? null 
                : Sprite.Create(value, new Rect(0, 0, value!.width, value.height), new Vector2(0.5f, 0.5f));
            
            SetValue(sprite);
        }
    }
}