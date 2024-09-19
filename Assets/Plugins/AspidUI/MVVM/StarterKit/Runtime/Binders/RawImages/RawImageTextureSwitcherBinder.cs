using UnityEngine;
using UnityEngine.UI;

namespace AspidUI.MVVM.StarterKit.Binders.RawImages
{
    public sealed class RawImageTextureSwitcherBinder : SwitcherBinder<Texture2D>
    {
        private readonly RawImage _image;

        public RawImageTextureSwitcherBinder(RawImage image, Texture2D trueValue, Texture2D falseValue) :
            base(trueValue, falseValue)
        {
            _image = image;
        }

        protected override void SetValue(Texture2D value) =>
            _image.texture = value;
    }
}