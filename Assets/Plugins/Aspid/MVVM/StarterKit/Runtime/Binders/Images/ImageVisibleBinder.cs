using System;
using UnityEngine.UI;

namespace Aspid.MVVM.StarterKit.Binders
{
    public class ImageVisibleBinder : Binder, IBinder<bool>
    {
        private readonly Image _image;
        private readonly bool _isInvert;

        public ImageVisibleBinder(Image image, bool isInvert = false)
        {
            _isInvert = isInvert;
            _image = image ?? throw new ArgumentNullException(nameof(image));
        }
        
        public void SetValue(bool value)
        {
            if (_isInvert) value = !_isInvert;
            _image.enabled = value;
        }
    }
}