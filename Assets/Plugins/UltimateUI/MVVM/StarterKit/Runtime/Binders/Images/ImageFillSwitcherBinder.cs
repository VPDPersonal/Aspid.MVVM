using System;
using UnityEngine.UI;

namespace UltimateUI.MVVM.StarterKit.Binders.Images
{
    public sealed class ImageFillSwitcherBinder : SwitcherBinder<float>
    {
        private readonly Image _image;

        public ImageFillSwitcherBinder(Image image, float trueValue, float falseValue)
            : base(trueValue, falseValue)
        {
            if (trueValue is < 0 or > 1) throw new ArgumentException($"{nameof(falseValue)} must be between 0 and 1.");
            if (falseValue is < 0 or > 1) throw new ArgumentException($"{nameof(falseValue)} must be between 0 and 1.");

            _image = image;
        }

        protected override void SetValue(float value) =>
            _image.fillAmount = value;
    }
}