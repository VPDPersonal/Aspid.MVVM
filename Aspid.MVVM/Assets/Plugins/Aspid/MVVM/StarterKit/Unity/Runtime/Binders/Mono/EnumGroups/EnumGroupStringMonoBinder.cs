#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<string, string>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterString;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="EnumGroupMonoBinder{TElement, string, IConverter{string, string}}"/> that fixes
    /// the value type to <see cref="string"/>.
    /// </summary>
    /// <typeparam name="TElement">The type of element in the group that receives the selected or default value.</typeparam>
    public abstract class EnumGroupStringMonoBinder<TElement> : EnumGroupMonoBinder<TElement, string, Converter> { }
}