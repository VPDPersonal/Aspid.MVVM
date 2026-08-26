using UnityEngine;
using Converter = Aspid.MVVM.StarterKit.IConverter<string, string>;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="EnumMonoBinder{T1, T2, T3}">EnumMonoBinder&lt;TComponent, string, IConverter&lt;string, string&gt;&gt;</see> that fixes
    /// the value type to <see cref="string"/>.
    /// </summary>
    /// <typeparam name="TComponent">The type of <see cref="Component"/> that exposes the target property.</typeparam>
    public abstract class EnumStringMonoBinder<TComponent> : EnumMonoBinder<TComponent, string, Converter>
        where TComponent : Component { }
}