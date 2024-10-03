using UnityEngine;
using UnityEngine.UI;

namespace Aspid.UI.MVVM.StarterKit.Binders.Images
{
    public class ImageSpriteBinder : Binder, IBinder<Sprite>
    {
        protected readonly Image Image;
        protected readonly bool DisabledWhenNull;

        public ImageSpriteBinder(Image image, bool disabledWhenNull = true)
        {
            Image = image;
            DisabledWhenNull = disabledWhenNull;
        }

        public void SetValue(Sprite value)
        {
            Image.sprite = value;
            if (DisabledWhenNull) Image.enabled = value != null;
        }
    }
}