// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    public interface IConverter<in TFrom, out TTo>
    {
        public TTo Convert(TFrom value);
    }
}