using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<string, string>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterString;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="SwitcherMonoBinder{TComponent, string, IConverter{string, string}}"/> that fixes
    /// the value type to <see cref="string"/>.
    /// </summary>
    /// <typeparam name="TComponent">The type of <see cref="Component"/> that exposes the target property.</typeparam>
    public abstract class SwitcherStringMonoBinder<TComponent> : SwitcherMonoBinder<TComponent, string, Converter>
        where TComponent : Component { }
}
