// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="EnumGroupMonoBinderWithConverter{T1, T2}">EnumGroupMonoBinderWithConverter&lt;TElement, int&gt;</see> that fixes
    /// the value type to <see cref="int"/>.
    /// </summary>
    /// <typeparam name="TElement">The type of element in the group that receives the selected or default value.</typeparam>
    public abstract class EnumGroupIntMonoBinder<TElement> : EnumGroupMonoBinderWithConverter<TElement, int> { }
}