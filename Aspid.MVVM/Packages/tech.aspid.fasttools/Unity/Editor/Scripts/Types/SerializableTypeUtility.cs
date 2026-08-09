#nullable enable
using System;
using Aspid.FastTools.Editors;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Types.Editors
{
    /// <summary>
    /// Reflection helpers for working with <see cref="ISerializableType"/> wrapper fields
    /// (<see cref="SerializableType"/> / <see cref="SerializableType{T}"/>),
    /// including elements of arrays and <see cref="List{T}"/>.
    /// </summary>
    internal static class SerializableTypeUtility
    {
        /// <summary>
        /// True for an <see cref="ISerializableType"/> wrapper field, or an array / <see cref="List{T}"/> of them.
        /// </summary>
        internal static bool IsSerializableTypeField(Type fieldType) =>
            typeof(ISerializableType).IsAssignableFrom(fieldType.GetCollectionElementTypeOrSelf());

        /// <summary>
        /// Resolves the wrapper's <see cref="ISerializableType.BaseType"/> from a field's declared type,
        /// unwrapping arrays and <see cref="List{T}"/>. Returns <c>false</c> when the field is not
        /// an <see cref="ISerializableType"/> wrapper.
        /// </summary>
        internal static bool TryGetBaseType(Type fieldType, out Type? baseType)
        {
            var type = fieldType.GetCollectionElementTypeOrSelf();

            if (!typeof(ISerializableType).IsAssignableFrom(type))
            {
                baseType = null;
                return false;
            }

            // BaseType is an instance member, so instantiate the wrapper to read it — the interface
            // contract requires implementations to keep a public parameterless constructor for this.
            baseType = ((ISerializableType)Activator.CreateInstance(type)).BaseType;
            return true;
        }
    }
}
