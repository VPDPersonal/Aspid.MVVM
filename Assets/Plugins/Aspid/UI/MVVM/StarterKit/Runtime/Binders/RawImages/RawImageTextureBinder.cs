#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Aspid.UI.MVVM.StarterKit.Binders
{
    public class RawImageTextureBinder : Binder, IBinder<Texture2D?>, IBinder<Sprite?>
    {
        private readonly RawImage _image;
        private readonly bool _disabledWhenNull;

        public RawImageTextureBinder(RawImage image, bool disabledWhenNull = true)
        {
            _disabledWhenNull = disabledWhenNull;
            _image = image ?? throw new ArgumentNullException(nameof(image));
        }

        public void SetValue(Texture2D? value)
        {
            _image.texture = value;
            if (_disabledWhenNull) _image.enabled = value != null;
        }

        public void SetValue(Sprite? value) =>
            SetValue(value?.texture);
    }
}