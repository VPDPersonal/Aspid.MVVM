using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Color, UnityEngine.Color>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterColor;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    public abstract class ComponentColorMonoBinder<TComponent> : ComponentMonoBinder<TComponent, Color, Converter>, IColorBinder
        where TComponent : Component { }
}