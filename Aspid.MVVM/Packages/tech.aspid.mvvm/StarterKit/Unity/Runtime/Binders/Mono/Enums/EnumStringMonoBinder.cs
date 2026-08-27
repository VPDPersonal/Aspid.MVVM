using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="EnumMonoBinderWithConverter{T1, T2}">EnumMonoBinderWithConverter&lt;TComponent, string&gt;</see> that fixes
    /// the value type to <see cref="string"/>.
    /// </summary>
    /// <typeparam name="TComponent">The type of <see cref="Component"/> that exposes the target property.</typeparam>
    public abstract class EnumStringMonoBinder<TComponent> : EnumMonoBinderWithConverter<TComponent, string>
        where TComponent : Component { }
}