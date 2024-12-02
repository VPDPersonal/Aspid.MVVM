#nullable enable
using System;
using UnityEngine.UI;

namespace Aspid.UI.MVVM.StarterKit.Binders
{
    public class RawImageVisibleBinder : Binder, IBinder<bool>
    {
        private readonly RawImage _image;
        private readonly bool _isInvert;

        public RawImageVisibleBinder(RawImage image, bool isInvert = false)
        {
            _isInvert = isInvert;
            _image = image ?? throw new ArgumentNullException(nameof(image));
        }
        
        public void SetValue(bool value)
        {
            value = _isInvert ? !value : value;
            _image.enabled = value;
        }
    }
}