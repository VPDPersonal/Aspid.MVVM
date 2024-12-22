#nullable enable
using System;

namespace Aspid.MVVM.StarterKit.Converters
{
    public class GenericFuncConverter<TFrom, TTo> : IConverter<TFrom?, TTo?>
    {
        private readonly Func<TFrom?, TTo?> _converter;
        
        public GenericFuncConverter(Func<TFrom?, TTo?> converter)
        {
            _converter = converter ?? throw new ArgumentNullException(nameof(converter));
        }

        public TTo? Convert(TFrom? value) => _converter(value);

        public Func<TFrom?, TTo?> GetFunc() => _converter;
    }
}