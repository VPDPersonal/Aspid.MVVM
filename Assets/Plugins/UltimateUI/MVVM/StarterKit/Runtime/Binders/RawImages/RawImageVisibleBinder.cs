using UnityEngine.UI;

namespace UltimateUI.MVVM.StarterKit.Binders.RawImages
{
    public class RawImageVisibleBinder : Binder, IBinder<bool>
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
    }
}