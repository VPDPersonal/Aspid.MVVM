using UnityEngine;
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Color, UnityEngine.Color>;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="ComponentMonoBinder{TComponent, Color, IConverter{Color, Color}}"/> that binds a <see cref="Color"/> property and implements <see cref="IColorBinder"/>.
    /// </summary>
    /// <typeparam name="TComponent">The type of <see cref="Component"/> that exposes the target <see cref="Color"/> property.</typeparam>
    public abstract class ComponentColorMonoBinder<TComponent> : ComponentMonoBinder<TComponent, Color, Converter>, IColorBinder
        where TComponent : Component { }
}