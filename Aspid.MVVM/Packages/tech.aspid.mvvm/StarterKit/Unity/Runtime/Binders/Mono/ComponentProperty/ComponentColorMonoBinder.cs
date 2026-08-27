using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="ComponentMonoBinderWithConverter{T1, T2}">ComponentMonoBinderWithConverter&lt;TComponent, Color&gt;</see> that binds a <see cref="Color"/> property and implements <see cref="IColorBinder"/>.
    /// </summary>
    /// <typeparam name="TComponent">The type of <see cref="Component"/> that exposes the target <see cref="Color"/> property.</typeparam>
    public abstract class ComponentColorMonoBinder<TComponent> : ComponentMonoBinderWithConverter<TComponent, Color>, IColorBinder
        where TComponent : Component { }
}