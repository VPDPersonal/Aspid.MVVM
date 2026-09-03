#nullable enable
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Caches the id a shader property name resolves to.
    /// </summary>
    public struct ShaderPropertyId
    {
        private int? _id;
        private string? _resolvedFrom;

        /// <summary>
        /// Returns the id <paramref name="name"/> resolves to, resolving it once per name.
        /// </summary>
        /// <param name="name">The shader property name, as the shader declares it.</param>
        /// <returns>The id the name resolves to.</returns>
        /// <remarks>
        /// Re-resolves when the name changes, which the Inspector allows at any time.
        /// </remarks>
        public int Resolve(string name)
        {
            if (_id.HasValue && _resolvedFrom == name) return _id.Value;

            _resolvedFrom = name;
            _id = Shader.PropertyToID(name);

            return _id.Value;
        }
    }
}
