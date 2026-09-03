#nullable enable
using System;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="Binder{TProperty}">Binder&lt;TObject&gt;</see> that binds a <see cref="Object">UnityEngine.Object</see>
    /// reference, normalizing destroyed references to <see langword="null"/> in both directions.
    /// </summary>
    /// <typeparam name="TObject">The type of <see cref="Object">UnityEngine.Object</see> the property holds.</typeparam>
    [Serializable]
    public abstract class ObjectBinder<TObject> : Binder<TObject>
        where TObject : Object
    {
        /// <remarks>
        /// For deserialization only: Unity assigns the fields itself.
        /// </remarks>
        protected ObjectBinder() { }

        /// <inheritdoc/>
        protected ObjectBinder(
            IConverter<TObject?, TObject?>? converter,
            BindMode mode = BindMode.OneWay)
            : base(converter, mode) { }

        /// <inheritdoc/>
        protected override TObject? GetConvertedValue(TObject? value)
        {
            var converted = base.GetConvertedValue(value);
            return converted ? converted : null;
        }

        /// <inheritdoc/>
        protected override TObject? GetConvertedBackValue(TObject? value)
        {
            var converted = base.GetConvertedBackValue(value);
            return converted ? converted : null;
        }
    }
}
