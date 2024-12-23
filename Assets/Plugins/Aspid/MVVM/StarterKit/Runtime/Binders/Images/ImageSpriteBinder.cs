#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public class ImageSpriteBinder : TargetBinder<Image>, IBinder<Sprite?>, IBinder<Texture2D?>
    {
        [Header("Parameter")]
        [SerializeField] private bool _disabledWhenNull;

        public ImageSpriteBinder(Image target, bool disabledWhenNull = true)
            : base(target)
        {
            _disabledWhenNull = disabledWhenNull;
        }

        public void SetValue(Sprite? value)
        {
            Target.sprite = value;
            if (_disabledWhenNull) Target.enabled = value is not null;
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