using UnityEngine;
using Converter = Aspid.MVVM.StarterKit.IConverter<float, float>;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="EnumMonoBinder{TComponent, float, IConverter{float, float}}"/> that fixes
    /// the value type to <see cref="float"/>.
    /// </summary>
    /// <typeparam name="TComponent">The type of <see cref="Component"/> that exposes the target property.</typeparam>
    public abstract class EnumFloatMonoBinder<TComponent> : EnumMonoBinder<TComponent, float, Converter>
        where TComponent : Component { }
}