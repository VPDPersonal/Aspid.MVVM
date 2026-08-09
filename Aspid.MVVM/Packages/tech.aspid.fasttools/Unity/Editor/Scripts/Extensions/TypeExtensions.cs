using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Editors
{
    /// <summary>
    /// Editor-side extension methods for <see cref="Type"/> shared across the editor tooling.
    /// </summary>
    internal static class TypeExtensions
    {
        /// <summary>
        /// Unwraps the element type of array or <see cref="List{T}"/>;
        /// returns <paramref name="type"/> unchanged when it is neither.
        /// </summary>
        /// <remarks>
        /// <see cref="List{T}"/> is matched by its open definition, so a single-argument generic wrapper
        /// is not mistaken for a collection and unwrapped by accident.
        /// </remarks>
        internal static Type GetCollectionElementTypeOrSelf(this Type type)
        {
            if (type.IsArray) return type.GetElementType();

            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>)
                ? type.GetGenericArguments()[0]
                : type;
        }
    }
}
