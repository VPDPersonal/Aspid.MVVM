using Converter = Aspid.MVVM.StarterKit.IConverter<float, float>;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="EnumGroupMonoBinder{T1, T2, T3}">EnumGroupMonoBinder&lt;TElement, float, IConverter&lt;float, float&gt;&gt;</see> that fixes
    /// the value type to <see cref="float"/>.
    /// </summary>
    /// <typeparam name="TElement">The type of element in the group that receives the selected or default value.</typeparam>
    public abstract class EnumGroupFloatMonoBinder<TElement> : EnumGroupMonoBinder<TElement, float, Converter> { }
}