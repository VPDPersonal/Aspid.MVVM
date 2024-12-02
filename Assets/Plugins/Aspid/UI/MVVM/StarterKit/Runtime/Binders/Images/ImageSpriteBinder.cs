#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Aspid.UI.MVVM.StarterKit.Binders
{
    public class ImageSpriteBinder : Binder, IBinder<Sprite?>
    {
        private readonly Image _image;
        private readonly bool _disabledWhenNull;

        public ImageSpriteBinder(Image image, bool disabledWhenNull = true)
        {
            _disabledWhenNull = disabledWhenNull;
            _image = image ?? throw new ArgumentNullException(nameof(image));
        }

        public void SetValue(Sprite? value)
        {
            _image.sprite = value;
            if (_disabledWhenNull) _image.enabled = value != null;
        }
    }
}