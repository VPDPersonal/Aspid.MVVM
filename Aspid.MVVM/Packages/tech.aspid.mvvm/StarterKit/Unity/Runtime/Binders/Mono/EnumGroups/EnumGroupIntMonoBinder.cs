#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<int, int>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterInt;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="EnumGroupMonoBinder{T1, T2, T3}">EnumGroupMonoBinder&lt;TElement, int, IConverter&lt;int, int&gt;&gt;</see> that fixes
    /// the value type to <see cref="int"/>.
    /// </summary>
    /// <typeparam name="TElement">The type of element in the group that receives the selected or default value.</typeparam>
    public abstract class EnumGroupIntMonoBinder<TElement> : EnumGroupMonoBinder<TElement, int, Converter> { }
}