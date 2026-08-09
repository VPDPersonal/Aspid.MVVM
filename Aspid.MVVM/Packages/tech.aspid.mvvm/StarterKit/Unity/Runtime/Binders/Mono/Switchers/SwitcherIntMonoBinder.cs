using UnityEngine;
using Converter = Aspid.MVVM.StarterKit.IConverter<int, int>;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="SwitcherMonoBinder{TComponent, int, IConverter{int, int}}"/> that fixes
    /// the value type to <see cref="int"/>.
    /// </summary>
    /// <typeparam name="TComponent">The type of <see cref="Component"/> that exposes the target property.</typeparam>
    public abstract class SwitcherIntMonoBinder<TComponent> : SwitcherMonoBinder<TComponent, int, Converter>
        where TComponent : Component { }
}