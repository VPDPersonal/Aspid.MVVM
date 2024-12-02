#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Aspid.UI.MVVM.StarterKit.Binders
{
    public sealed class ImageSpriteSwitcherBinder : SwitcherBinder<Sprite?>
    {
        private readonly Image _image;

        public ImageSpriteSwitcherBinder(Image image, Sprite trueValue, Sprite falseValue) : base(trueValue, falseValue)
        {
            _image = image ?? throw new ArgumentNullException();
        }

        protected override void SetValue(Sprite? value) =>
            _image.sprite = value;
    }
}