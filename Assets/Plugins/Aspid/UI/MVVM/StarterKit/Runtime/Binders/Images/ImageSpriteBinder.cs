using UnityEngine;
using UnityEngine.UI;

namespace Aspid.UI.MVVM.StarterKit.Binders.Images
{
    public class ImageSpriteBinder : Binder, IBinder<Sprite>
    {
        protected readonly Image Image;

        public ImageSpriteBinder(Image image)
        {
            Image = image;
        }

        public void SetValue(Sprite value) =>
            Image.sprite = value;
    }
}