using System;

namespace Aspid.UI.MVVM.StarterKit.Converters
{
    public class GenericFuncConverter<TFrom, TTo> : IConverter<TFrom, TTo>
    {
        private readonly Func<TFrom, TTo> _converter;
        
        public GenericFuncConverter(Func<TFrom, TTo> converter)
        {
            _converter = converter;
        }

        public TTo Convert(TFrom value) => _converter(value);
    }
}