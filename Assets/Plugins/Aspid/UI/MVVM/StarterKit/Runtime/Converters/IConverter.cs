using System;

namespace Aspid.UI.MVVM.StarterKit.Converters
{
    public interface IConverter<in TFrom, out TTo>
    {
        public TTo Convert(TFrom value);

        public Func<TFrom, TTo> GetFunc() => Convert;
    }
}