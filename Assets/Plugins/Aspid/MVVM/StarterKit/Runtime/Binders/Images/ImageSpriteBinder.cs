#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public class ImageSpriteBinder : Binder, IBinder<Sprite?>
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
    }
}