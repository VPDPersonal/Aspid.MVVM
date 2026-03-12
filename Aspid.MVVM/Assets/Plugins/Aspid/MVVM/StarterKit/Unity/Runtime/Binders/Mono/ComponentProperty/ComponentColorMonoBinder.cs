using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Color, UnityEngine.Color>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterColor;
#endif

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