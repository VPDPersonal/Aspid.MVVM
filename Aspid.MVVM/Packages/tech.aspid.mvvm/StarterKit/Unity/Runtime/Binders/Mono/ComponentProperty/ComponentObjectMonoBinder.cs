using Object = UnityEngine.Object;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="ComponentMonoBinder{TComponent,TProperty}">ComponentMonoBinder&lt;TComponent, TObject&gt;</see> that binds a
    /// property holding a reference to a <see cref="Object">UnityEngine.Object</see>, normalizing destroyed
    /// references to <see langword="null"/> in both binding directions.
    /// </summary>
    /// <remarks>
    /// A destroyed Unity object is not a <see langword="null"/> reference: the managed wrapper survives and compares
    /// equal to <see langword="null"/> only through <see cref="Object"/>'s own operators. Without this layer a
    /// ViewModel could hand over a destroyed asset — which the property would accept and the Inspector would show as
    /// <c>Missing</c> — or receive one back in <see cref="BindMode.OneWayToSource"/> and store it as a live value.
    /// </remarks>
    /// <typeparam name="TComponent">The type of <see cref="Component"/> that exposes the target property.</typeparam>
    /// <typeparam name="TObject">The type of <see cref="Object">UnityEngine.Object</see> the property holds.</typeparam>
    public abstract class ComponentObjectMonoBinder<TComponent, TObject> : ComponentMonoBinder<TComponent, TObject>
        where TComponent : Component
        where TObject : Object
    {
        /// <inheritdoc/>
        /// <remarks>
        /// Returns <see langword="null"/> when the converted value refers to a destroyed object.
        /// </remarks>
        protected override TObject GetConvertedValue(TObject value)
        {
            var converted = base.GetConvertedValue(value);
            return converted ? converted : null;
        }

        /// <inheritdoc/>
        /// <remarks>
        /// Returns <see langword="null"/> when the converted value refers to a destroyed object.
        /// </remarks>
        protected override TObject GetConvertedBackValue(TObject value)
        {
            var converted = base.GetConvertedBackValue(value);
            return converted ? converted : null;
        }
    }
}
