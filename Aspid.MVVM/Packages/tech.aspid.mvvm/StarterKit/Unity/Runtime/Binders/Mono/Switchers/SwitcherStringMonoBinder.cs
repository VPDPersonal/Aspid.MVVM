using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="SwitcherMonoBinderWithConverter{T1, T2}">SwitcherMonoBinderWithConverter&lt;TComponent, string&gt;</see> that fixes
    /// the value type to <see cref="string"/>.
    /// </summary>
    /// <typeparam name="TComponent">The type of <see cref="Component"/> that exposes the target property.</typeparam>
    public abstract class SwitcherStringMonoBinder<TComponent> : SwitcherMonoBinderWithConverter<TComponent, string>
        where TComponent : Component { }
}
