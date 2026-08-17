using UnityEngine;
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Color, UnityEngine.Color>;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="EnumGroupMonoBinder{TElement, Color, IConverter{Color, Color}}"/> that fixes
    /// the value type to <see cref="Color"/>.
    /// </summary>
    /// <typeparam name="TElement">The type of element in the group that receives the selected or default value.</typeparam>
    public abstract class EnumGroupColorMonoBinder<TElement> : EnumGroupMonoBinder<TElement, Color, Converter> { }
}