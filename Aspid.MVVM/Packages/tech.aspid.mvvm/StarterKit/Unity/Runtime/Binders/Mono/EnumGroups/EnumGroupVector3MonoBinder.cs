using UnityEngine;
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Vector3, UnityEngine.Vector3>;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="EnumGroupMonoBinder{TElement, Vector3, IConverter{Vector3, Vector3}}"/> that fixes
    /// the value type to <see cref="Vector3"/>.
    /// </summary>
    /// <typeparam name="TElement">The type of element in the group that receives the selected or default value.</typeparam>
    public abstract class EnumGroupVector3MonoBinder<TElement> : EnumGroupMonoBinder<TElement, Vector3, Converter> { }
}