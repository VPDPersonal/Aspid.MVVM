using UnityEngine;
using Converter = Aspid.MVVM.StarterKit.IConverter<float, float>;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="SwitcherMonoBinder{T1, T2, T3}">SwitcherMonoBinder&lt;TComponent, float, IConverter&lt;float, float&gt;&gt;</see> that fixes
    /// the value type to <see cref="float"/>.
    /// </summary>
    /// <typeparam name="TComponent">The type of <see cref="Component"/> that exposes the target property.</typeparam>
    public abstract class SwitcherFloatMonoBinder<TComponent> : SwitcherMonoBinder<TComponent, float, Converter>
        where TComponent : Component { }
}