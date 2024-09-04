using System;
using UnityEngine.UI;
using UltimateUI.MVVM.StarterKit.Converters;

namespace UltimateUI.MVVM.StarterKit.Binders.Images
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