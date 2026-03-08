using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Color, UnityEngine.Color>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterColor;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base MonoBehaviour binder for binding a <see cref="UnityEngine.Color"/> property on a Unity <see cref="UnityEngine.Component"/>.
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established the current color value is sent back to the ViewModel.
    /// Implements <see cref="IColorBinder"/> to accept color and component values.
    /// </summary>
    public abstract class ComponentColorMonoBinder<TComponent> : ComponentMonoBinder<TComponent, Color, Converter>, IColorBinder
        where TComponent : Component { }
}