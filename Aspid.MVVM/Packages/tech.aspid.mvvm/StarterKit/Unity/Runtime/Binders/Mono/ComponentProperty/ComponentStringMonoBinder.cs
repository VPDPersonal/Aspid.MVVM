using UnityEngine;
using Converter = Aspid.MVVM.StarterKit.IConverter<string, string>;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="ComponentMonoBinder{TComponent, string, IConverter{string, string}}"/> that binds an <see langword="string"/> property.
    /// </summary>
    /// <typeparam name="TComponent">The type of <see cref="Component"/> that exposes the target <see langword="string"/> property.</typeparam>
    public abstract class ComponentStringMonoBinder<TComponent> : ComponentMonoBinder<TComponent, string, Converter> 
        where TComponent : Component { }
}