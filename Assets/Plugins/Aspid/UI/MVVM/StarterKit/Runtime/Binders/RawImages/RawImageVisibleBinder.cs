using UnityEngine;
using UnityEngine.UI;

namespace Aspid.UI.MVVM.StarterKit.Binders.RawImages
{
    public class RawImageVisibleBinder : Binder, IBinder<bool>, IBinder<Texture2D>
    {
        protected readonly RawImage Image;
        protected readonly bool IsInvert;

        public RawImageVisibleBinder(RawImage image, bool isInvert = false)
        {
            Image = image;
            IsInvert = isInvert;
        }
        
        public void SetValue(bool value)
        {
            if (IsInvert) value = !IsInvert;
            Image.enabled = value;
        }

        public void SetValue(Texture2D texture)
        {
            var value = texture != null;
            SetValue(value);
        }
    }
}