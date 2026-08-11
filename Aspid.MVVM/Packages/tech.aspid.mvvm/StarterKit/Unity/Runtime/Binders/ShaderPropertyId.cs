using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Caches the id a shader property name resolves to.
    /// </summary>
    /// <remarks>
    /// <see cref="Shader.PropertyToID"/> hashes the string, so resolving it per value pays for the hash on every write.
    /// Six binders carried their own <c>int?</c> field and <c>??=</c> line to avoid that; this is the same two lines,
    /// once.
    /// <para/>
    /// The name itself stays where it is — a serialized field on the binder. Moving it in here would move it inside a
    /// nested struct in the serialized data and lose the value every project had set, which is the failure the converter
    /// rename in this package had to be reverted for.
    /// </remarks>
    public struct ShaderPropertyId
    {
        private int? _id;
        private string _resolvedFrom;

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
