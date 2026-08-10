using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// A converter from <see cref="float"/> to <see cref="float"/>.
    /// </summary>
    /// <remarks>
    /// A named alias for <see cref="IConverter{TFrom, TTo}"/> closed over these two types. It
    /// existed because Unity before 2023.1 could not serialize a <c>[SerializeReference]</c> field
    /// typed as an open generic, so a binder declared the field as this instead.
    /// <para>
    /// The package requires Unity 6000.0, so nothing in it declares such a field any more and this
    /// carries no behaviour of its own. It stays one release so that code naming it keeps compiling
    /// with a warning, and so that a <c>[SerializeReference]</c> field a project declares as this
    /// type does not silently deserialize to <see langword="null"/>.
    /// </para>
    /// </remarks>
    [Obsolete("Named converter aliases only existed because Unity before 2023.1 could not serialize a [SerializeReference] field of an open generic type. The package now requires Unity 6000.0, so use IConverter<float, float> directly. This will be removed in the next major version.")]
    public interface IConverterFloat : IConverter<float, float> { }
}