#nullable enable
using System;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="Binder{TProperty}">Binder&lt;TObject&gt;</see> that binds a
    /// property holding a reference to a <see cref="Object">UnityEngine.Object</see>, normalizing destroyed
    /// references to <see langword="null"/> in both binding directions.
    /// </summary>
    /// <remarks>
    /// A destroyed Unity object is not a <see langword="null"/> reference: the managed wrapper survives and compares
    /// equal to <see langword="null"/> only through <see cref="Object"/>'s own operators.
    /// </remarks>
    /// <typeparam name="TObject">The type of <see cref="Object">UnityEngine.Object</see> the property holds.</typeparam>
    [Serializable]
    public abstract class ObjectBinder<TObject> : Binder<TObject>
        where TObject : Object
    {
        /// <inheritdoc/>
        /// <remarks>
        /// For deserialization only: Unity builds a serialized instance without running a constructor's arguments and
        /// assigns the fields itself.
        /// </remarks>
        protected ObjectBinder() { }

        protected ObjectBinder(IConverter<TObject?, TObject?>? converter, BindMode mode = BindMode.OneWay)
            : base(converter, mode) { }

        /// <inheritdoc/>
        /// <remarks>
        /// Returns <see langword="null"/> when the converted value refers to a destroyed object.
        /// </remarks>
        protected override TObject? GetConvertedValue(TObject? value)
        {
            var converted = base.GetConvertedValue(value);
            return converted ? converted : null;
        }

        /// <inheritdoc/>
        /// <remarks>
        /// Returns <see langword="null"/> when the converted value refers to a destroyed object.
        /// </remarks>
        protected override TObject? GetConvertedBackValue(TObject? value)
        {
            var converted = base.GetConvertedBackValue(value);
            return converted ? converted : null;
        }
    }
}
