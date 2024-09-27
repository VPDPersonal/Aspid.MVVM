using UnityEngine;
using UnityEngine.UI;

namespace Aspid.UI.MVVM.StarterKit.Binders.Images
{
    public class ImageVisibleBinder : Binder, IBinder<bool>, IBinder<Sprite>
    {
        protected readonly Image Image;
        protected readonly bool IsInvert;

        public ImageVisibleBinder(Image image, bool isInvert = false)
        {
            Image = image;
            IsInvert = isInvert;
        }
        
        public void SetValue(bool value)
        {
            if (IsInvert) value = !IsInvert;
            Image.enabled = value;
        }

        public void SetValue(Sprite sprite)
        {
            var value = sprite != null;
            SetValue(value);
        }
    }
}