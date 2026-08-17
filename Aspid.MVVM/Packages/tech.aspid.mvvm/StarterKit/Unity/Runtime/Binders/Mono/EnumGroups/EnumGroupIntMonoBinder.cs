using Converter = Aspid.MVVM.StarterKit.IConverter<int, int>;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="EnumGroupMonoBinder{TElement, int, IConverter{int, int}}"/> that fixes
    /// the value type to <see cref="int"/>.
    /// </summary>
    /// <typeparam name="TElement">The type of element in the group that receives the selected or default value.</typeparam>
    public abstract class EnumGroupIntMonoBinder<TElement> : EnumGroupMonoBinder<TElement, int, Converter> { }
}