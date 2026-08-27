using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="EnumGroupMonoBinderWithConverter{T1, T2}">EnumGroupMonoBinderWithConverter&lt;TElement, Color&gt;</see> that fixes
    /// the value type to <see cref="Color"/>.
    /// </summary>
    /// <typeparam name="TElement">The type of element in the group that receives the selected or default value.</typeparam>
    public abstract class EnumGroupColorMonoBinder<TElement> : EnumGroupMonoBinderWithConverter<TElement, Color> { }
}