#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Binder that sets the <see cref="UnityEngine.UI.Image.sprite"/> property on an <see cref="UnityEngine.UI.Image"/>
    /// when the bound ViewModel value changes. Optionally disables the Image when the sprite is null.
    /// </summary>
    [Serializable]
    public class ImageSpriteBinder : TargetBinder<Image, Sprite>, IBinder<Texture2D?>
    {
        [SerializeField] private bool _disabledWhenNull;

        protected sealed override Sprite? Property
        {
            get => Target.sprite;
            set
            {
                Target.sprite = value;
                Target.enabled = !_disabledWhenNull || value;
            }
        }

        public ImageSpriteBinder(Image target, BindMode mode)
            : this(target, disabledWhenNull: true, mode) { }

        public ImageSpriteBinder(Image target, bool disabledWhenNull = true, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
            _disabledWhenNull = disabledWhenNull;
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