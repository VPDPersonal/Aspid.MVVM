#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public sealed class ImageFillSwitcherBinder : SwitcherBinder<float>
    {
        [Header("Component")]
        [SerializeField] private Image _image;

        public ImageFillSwitcherBinder(Image image, float trueValue, float falseValue)
            : base(trueValue, falseValue)
        {
            if (trueValue is < 0 or > 1) throw new ArgumentException($"{nameof(falseValue)} must be between 0 and 1.");
            if (falseValue is < 0 or > 1) throw new ArgumentException($"{nameof(falseValue)} must be between 0 and 1.");

            _image = image ?? throw new ArgumentNullException(nameof(image));
        }

        protected override void SetValue(float value) =>
            _image.fillAmount = value;
    }
}