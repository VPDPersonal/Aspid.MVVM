#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public class RawImageTextureBinder : Binder, IBinder<Texture2D?>, IBinder<Sprite?>
    {
        [Header("Component")]
        [SerializeField] private RawImage _image;
        
        [Header("Parameter")]
        [SerializeField] private bool _disabledWhenNull;

        public RawImageTextureBinder(RawImage image, bool disabledWhenNull = true)
        {
            _disabledWhenNull = disabledWhenNull;
            _image = image ?? throw new ArgumentNullException(nameof(image));
        }

        public void SetValue(Texture2D? value)
        {
            _image.texture = value;
            if (_disabledWhenNull) _image.enabled = value is not null;
        }

        public void SetValue(Sprite? value) =>
            SetValue(value?.texture);
    }
}