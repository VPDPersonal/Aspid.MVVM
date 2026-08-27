using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="SwitcherMonoBinderWithConverter{T1, T2}">SwitcherMonoBinderWithConverter&lt;TComponent, Color&gt;</see> that fixes
    /// the value type to <see cref="Color"/>.
    /// </summary>
    /// <typeparam name="TComponent">The type of <see cref="Component"/> that exposes the target property.</typeparam>
    public abstract class SwitcherColorMonoBinder<TComponent> : SwitcherMonoBinderWithConverter<TComponent, Color>
        where TComponent : Component { }
}