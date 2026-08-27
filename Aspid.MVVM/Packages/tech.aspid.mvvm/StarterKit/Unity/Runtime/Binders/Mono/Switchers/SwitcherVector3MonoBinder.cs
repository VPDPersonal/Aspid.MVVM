using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="SwitcherMonoBinderWithConverter{T1, T2}">SwitcherMonoBinderWithConverter&lt;TComponent, Vector3&gt;</see> that fixes
    /// the value type to <see cref="Vector3"/>.
    /// </summary>
    /// <typeparam name="TComponent">The type of <see cref="Component"/> that exposes the target property.</typeparam>
    public abstract class SwitcherVector3MonoBinder<TComponent> : SwitcherMonoBinderWithConverter<TComponent, Vector3>
        where TComponent : Component { }
}