#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Aspid.MVVM.StarterKit.Binders
{
    public sealed class RawImageMaterialSwitcherBinder : SwitcherBinder<Material>
    {
        private readonly RawImage _image;

        public RawImageMaterialSwitcherBinder(RawImage image, Material trueValue, Material falseValue ) 
            : base(trueValue, falseValue)
        {
            _image = image ?? throw new ArgumentNullException(nameof(image));
        }

        protected override void SetValue(Material value) =>
            _image.material = value;
    }
}