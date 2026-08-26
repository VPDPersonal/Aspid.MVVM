using UnityEngine;
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Color, UnityEngine.Color>;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="SwitcherMonoBinder{T1, T2, T3}">SwitcherMonoBinder&lt;TComponent, Color, IConverter&lt;Color, Color&gt;&gt;</see> that fixes
    /// the value type to <see cref="Color"/>.
    /// </summary>
    /// <typeparam name="TComponent">The type of <see cref="Component"/> that exposes the target property.</typeparam>
    public abstract class SwitcherColorMonoBinder<TComponent> : SwitcherMonoBinder<TComponent, Color, Converter>
        where TComponent : Component { }
}