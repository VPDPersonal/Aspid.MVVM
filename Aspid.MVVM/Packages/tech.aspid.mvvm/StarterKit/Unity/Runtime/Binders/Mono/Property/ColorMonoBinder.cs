using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="MonoBinderWithConverter{TProperty}">MonoBinderWithConverter&lt;Color&gt;</see> that binds a <see cref="Color"/> property and implements <see cref="IColorBinder"/>.
    /// </summary>
    public abstract class ColorMonoBinder : MonoBinderWithConverter<Color>, IColorBinder { }
}
