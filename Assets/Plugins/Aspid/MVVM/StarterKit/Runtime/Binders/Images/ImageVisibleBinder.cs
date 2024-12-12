using System;
using UnityEngine;
using UnityEngine.UI;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public class ImageVisibleBinder : Binder, IBinder<bool>
    {
        [Header("Component")]
        [SerializeField] private Image _image;
        
        [Header("Parameter")]
        [SerializeField] private bool _isInvert;

        public ImageVisibleBinder(Image image, bool isInvert = false)
        {
            _isInvert = isInvert;
            _image = image ?? throw new ArgumentNullException(nameof(image));
        }
        
        public void SetValue(bool value) =>
            _image.enabled = _isInvert ? !value : value;
    }
}