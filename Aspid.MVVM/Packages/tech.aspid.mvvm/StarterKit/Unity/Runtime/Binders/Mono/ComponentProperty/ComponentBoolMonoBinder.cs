using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="ComponentMonoBinderWithConverter{T1, T2}">ComponentMonoBinderWithConverter&lt;TComponent, bool&gt;</see> that binds a <see langword="bool"/> property.
    /// </summary>
    /// <typeparam name="TComponent">The type of <see cref="Component"/> that exposes the target <see langword="bool"/> property.</typeparam>
    public abstract class ComponentBoolMonoBinder<TComponent> : ComponentMonoBinderWithConverter<TComponent, bool>
        where TComponent : Component
    {
    }
}
