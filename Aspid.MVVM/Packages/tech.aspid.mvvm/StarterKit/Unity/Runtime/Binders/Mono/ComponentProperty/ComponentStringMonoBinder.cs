using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="ComponentMonoBinderWithConverter{T1, T2}">ComponentMonoBinderWithConverter&lt;TComponent, string&gt;</see> that binds a <see langword="string"/> property.
    /// </summary>
    /// <typeparam name="TComponent">The type of <see cref="Component"/> that exposes the target <see langword="string"/> property.</typeparam>
    public abstract class ComponentStringMonoBinder<TComponent> : ComponentMonoBinderWithConverter<TComponent, string> 
        where TComponent : Component { }
}