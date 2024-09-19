using UnityEngine;
using UnityEngine.UI;

namespace AspidUI.MVVM.StarterKit.Binders.Images
{
    public sealed class ImageSpriteSwitcherBinder : SwitcherBinder<Sprite>
    {
        private readonly Image _image;

        public ImageSpriteSwitcherBinder(Image image, Sprite trueValue, Sprite falseValue) : base(trueValue, falseValue)
        {
            _image = image;
        }

        protected override void SetValue(Sprite value) =>
            _image.sprite = value;
    }
}