using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="EnumGroupMonoBinderWithConverter{T1, T2}">EnumGroupMonoBinderWithConverter&lt;TElement, Vector3&gt;</see> that fixes
    /// the value type to <see cref="Vector3"/>.
    /// </summary>
    /// <typeparam name="TElement">The type of element in the group that receives the selected or default value.</typeparam>
    public abstract class EnumGroupVector3MonoBinder<TElement> : EnumGroupMonoBinderWithConverter<TElement, Vector3> { }
}