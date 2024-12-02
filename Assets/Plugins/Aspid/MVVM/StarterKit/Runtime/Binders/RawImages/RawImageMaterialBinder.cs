#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders
{
    public class RawImageMaterialBinder : Binder, IBinder<Material?>
    {
        private readonly RawImage _image;
        private readonly IConverter<Material?, Material>? _converter;

        public RawImageMaterialBinder(RawImage image, Func<Material?, Material> converter)
            : this(image, new GenericFuncConverter<Material?, Material>(converter)) { }
        
        public RawImageMaterialBinder(RawImage image, IConverter<Material?, Material>? converter = null)
        {
            _converter = converter;
            _image = image ?? throw new ArgumentNullException(nameof(image));
        }

        public void SetValue(Material? value) =>
            _image.material = _converter?.Convert(value) ?? value;
    }
}