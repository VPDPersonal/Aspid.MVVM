namespace Aspid.MVVM.StarterKit.Converters
{
    public interface IConverter<in TFrom, out TTo>
    {
        public TTo Convert(TFrom value);
    }
}