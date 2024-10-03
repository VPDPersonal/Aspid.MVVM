using UnityEngine;
using UnityEngine.UI;

namespace Aspid.UI.MVVM.StarterKit.Binders.RawImages
{
    public class RawImageTextureBinder : Binder, IBinder<Texture2D>
    {
        protected readonly RawImage Image;
        protected readonly bool DisabledWhenNull;

        public RawImageTextureBinder(RawImage image, bool disabledWhenNull = true)
        {
            Image = image;
            DisabledWhenNull = false;
        }

        public void SetValue(Texture2D value)
        {
            Image.texture = value;
            if (DisabledWhenNull) Image.enabled = value != null;
        }
    }
}