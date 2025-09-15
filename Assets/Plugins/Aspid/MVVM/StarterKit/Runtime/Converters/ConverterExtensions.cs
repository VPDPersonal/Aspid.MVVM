using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    public static class ConverterExtensions
    {
        public static IConverter<TFrom?, TTo?> ToConvert<TFrom, TTo>(this Func<TFrom?, TTo?> converter) =>
            new GenericFuncConverter<TFrom, TTo>(converter);
    }
}