using UnityEngine;
using UnityEngine.UI;

namespace UltimateUI.MVVM.StarterKit.Binders.RawImages
{
    public class RawImageTextureBinder : Binder, IBinder<Texture2D>
    {
        protected readonly RawImage Image;

        public RawImageTextureBinder(RawImage image)
        {
            Image = image;
        }

        public void SetValue(Texture2D value) =>
            Image.texture = value;
    }
}