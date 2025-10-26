#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public class ImageSpriteBinder : TargetBinder<Image>, IBinder<Sprite?>, IBinder<Texture2D?>
    {
        [SerializeField] private bool _disabledWhenNull;
        
        public ImageSpriteBinder(Image target, BindMode mode)
            : this(target, true, mode) { }
        
        public ImageSpriteBinder(Image target, bool disabledWhenNull = true, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
            _disabledWhenNull = disabledWhenNull;
        }

        public void SetValue(Sprite? value)
        {
            Target.sprite = value;
            Target.enabled = !_disabledWhenNull || value;
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