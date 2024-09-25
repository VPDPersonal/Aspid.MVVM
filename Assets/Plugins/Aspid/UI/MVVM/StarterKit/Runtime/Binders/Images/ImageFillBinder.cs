using System;
using UnityEngine.UI;
using Aspid.UI.MVVM.StarterKit.Converters;

namespace Aspid.UI.MVVM.StarterKit.Binders.Images
{
    public class ImageFillBinder : Binder, IBinder<float>
    {
        protected readonly Image Image;
        protected readonly IConverter<float, float> Converter;

        public ImageFillBinder(Image image, Func<float, float> converter) 
            : this(image, new GenericFuncConverter<float, float>(converter)) { }
        
        public ImageFillBinder(Image image, IConverter<float, float> converter = null)
        {
            Image = image;
            Converter = converter;
        }

        public void SetValue(float value) =>
            Image.fillAmount = Converter?.Convert(value) ?? value;
    }
}