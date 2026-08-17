#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// A converter from <see cref="Vector2"/> to <see cref="Vector2"/>.
    /// </summary>
    /// <remarks>
    /// Kept for one release so code naming it still compiles and a <c>[SerializeReference]</c> field
    /// a project declares as this type does not silently deserialize to <see langword="null"/>.
    /// </remarks>
    [Obsolete("Named converter aliases only existed because Unity before 2023.1 could not serialize a [SerializeReference] field of an open generic type. The package now requires Unity 6000.0, so use IConverter<Vector2, Vector2> directly. This will be removed in the next major version.")]
    public interface IConverterVector2 : IConverter<Vector2, Vector2> { }
}