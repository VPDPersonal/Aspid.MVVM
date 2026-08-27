using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="EnumMonoBinderWithConverter{T1, T2}">EnumMonoBinderWithConverter&lt;TComponent, Vector3&gt;</see> that fixes
    /// the value type to <see cref="Vector3"/>.
    /// </summary>
    /// <typeparam name="TComponent">The type of <see cref="Component"/> that exposes the target property.</typeparam>
    public abstract class EnumVector3MonoBinder<TComponent> : EnumMonoBinderWithConverter<TComponent, Vector3>
        where TComponent : Component { }
}