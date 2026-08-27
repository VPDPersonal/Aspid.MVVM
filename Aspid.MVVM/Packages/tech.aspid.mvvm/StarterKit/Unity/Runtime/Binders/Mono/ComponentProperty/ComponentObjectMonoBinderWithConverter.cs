using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="ComponentMonoBinderWithConverter{T1, T2}">ComponentMonoBinderWithConverter&lt;TComponent, TObject&gt;</see> that binds a
    /// property holding a reference to a <see cref="Object">UnityEngine.Object</see>, normalizing destroyed
    /// references to <see langword="null"/> after the configured converter has run.
    /// </summary>
    /// <remarks>
    /// The converter runs first, so a converter that resolves one asset into another is still checked: whatever it
    /// returns is what reaches the property. See <see cref="ComponentObjectMonoBinder{TComponent, TObject}"/> for why
    /// the check is needed at all.
    /// </remarks>
    /// <typeparam name="TComponent">The type of <see cref="Component"/> that exposes the target property.</typeparam>
    /// <typeparam name="TObject">The type of <see cref="Object">UnityEngine.Object</see> the property holds.</typeparam>
    public abstract class ComponentObjectMonoBinderWithConverter<TComponent, TObject> : ComponentMonoBinderWithConverter<TComponent, TObject>
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
    }
}
