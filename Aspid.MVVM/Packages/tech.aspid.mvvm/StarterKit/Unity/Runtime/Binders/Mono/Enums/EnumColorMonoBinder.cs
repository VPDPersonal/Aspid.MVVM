using UnityEngine;
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Color, UnityEngine.Color>;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="EnumMonoBinder{TComponent, Color, IConverter{Color, Color}}"/> that fixes
    /// the value type to <see cref="Color"/>.
    /// </summary>
    /// <typeparam name="TComponent">The type of <see cref="Component"/> that exposes the target property.</typeparam>
    public abstract class EnumColorMonoBinder<TComponent> : EnumMonoBinder<TComponent, Color, Converter>
        where TComponent : Component { }
}