using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="SwitcherMonoBinderWithConverter{T1, T2}">SwitcherMonoBinderWithConverter&lt;TComponent, float&gt;</see> that fixes
    /// the value type to <see cref="float"/>.
    /// </summary>
    /// <typeparam name="TComponent">The type of <see cref="Component"/> that exposes the target property.</typeparam>
    public abstract class SwitcherFloatMonoBinder<TComponent> : SwitcherMonoBinderWithConverter<TComponent, float>
        where TComponent : Component { }
}