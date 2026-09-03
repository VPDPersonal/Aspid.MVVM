using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="ComponentMonoBinder{TComponent, TProperty}">ComponentMonoBinder&lt;TComponent, TObject&gt;</see> that binds
    /// a <see cref="Object">UnityEngine.Object</see> reference, normalizing destroyed references to <see langword="null"/> in both directions.
    /// </summary>
    /// <typeparam name="TComponent">The type of <see cref="Component"/> that exposes the bound property.</typeparam>
    /// <typeparam name="TObject">The type of <see cref="Object">UnityEngine.Object</see> the property holds.</typeparam>
    public abstract class ComponentObjectMonoBinder<TComponent, TObject> : ComponentMonoBinder<TComponent, TObject>
        where TComponent : Component
        where TObject : Object
    {
        /// <inheritdoc/>
        protected override TObject GetConvertedValue(TObject value)
        {
            var converted = base.GetConvertedValue(value);
            return converted ? converted : null;
        }

        /// <inheritdoc/>
        protected override TObject GetConvertedBackValue(TObject value)
        {
            var converted = base.GetConvertedBackValue(value);
            return converted ? converted : null;
        }
    }
}
