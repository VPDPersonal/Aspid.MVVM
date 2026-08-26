using Converter = Aspid.MVVM.StarterKit.IConverter<string, string>;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="EnumGroupMonoBinder{T1, T2, T3}">EnumGroupMonoBinder&lt;TElement, string, IConverter&lt;string, string&gt;&gt;</see> that fixes
    /// the value type to <see cref="string"/>.
    /// </summary>
    /// <typeparam name="TElement">The type of element in the group that receives the selected or default value.</typeparam>
    public abstract class EnumGroupStringMonoBinder<TElement> : EnumGroupMonoBinder<TElement, string, Converter> { }
}