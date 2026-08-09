#nullable enable
using System;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Types
{
    /// <summary>
    /// Common contract of the serializable <see cref="System.Type"/> wrappers
    /// (<see cref="SerializableType"/> and <see cref="SerializableType{T}"/>).
    /// </summary>
    public interface ISerializableType
    {
        /// <summary>
        /// The constraint that stored types must satisfy — candidate types offered
        /// by the editor picker are assignable to it; <see cref="object"/> when unconstrained.
        /// </summary>
        public Type BaseType { get; }

        /// <summary>
        /// The resolved <see cref="System.Type"/>, or <c>null</c> when no type is stored
        /// or the stored assembly-qualified name cannot be resolved.
        /// </summary>
        public Type? Type { get; }
    }
}
