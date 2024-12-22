#nullable enable
using System;

namespace Aspid.MVVM.StarterKit.Converters
{
    public static class ConverterExtensions
    {
        public static Func<TFrom, TTo> ToFunc<TFrom, TTo>(this IConverter<TFrom, TTo> converter) =>
            converter.GetFunc();
        
        public static IConverter<TFrom?, TTo?> ToConvert<TFrom, TTo>(this Func<TFrom?, TTo?> converter) =>
            new GenericFuncConverter<TFrom, TTo>(converter);
    }
}