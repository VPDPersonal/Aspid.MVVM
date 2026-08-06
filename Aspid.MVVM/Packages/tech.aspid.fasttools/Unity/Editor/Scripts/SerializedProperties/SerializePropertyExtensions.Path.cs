using System;
using UnityEditor;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Editors
{
    public static partial class SerializePropertyExtensions
    {
        /// <summary>
        /// The name of the member that backs <paramref name="property"/>.
        /// </summary>
        /// <remarks>
        /// Equals <see cref="SerializedProperty.name"/> for a regular field, but differs for an array/list element:
        /// its path ends with <c>Array.data[i]</c>, so <c>name</c> is just <c>data</c> — here <c>_slots.Array.data[0]</c>
        /// yields the collection field's name <c>_slots</c> instead.
        /// </remarks>
        public static string GetMemberName(this SerializedProperty property)
        {
            var path = property.SimplifyPropertyPath();

            // "_a._slots[0]" -> "_slots[0]"   |   "_a._weapon" -> "_weapon"
            var lastSegment = path[(path.LastIndexOf('.') + 1)..];

            // "_slots[0]" -> "_slots"   |   "_weapon" -> "_weapon"
            var bracket = lastSegment.IndexOf('[');
            return bracket < 0 ? lastSegment : lastSegment[..bracket];
        }

        /// <summary>
        /// True when <paramref name="property"/> is an element of an array or <see cref="List{T}"/>
        /// — its <see cref="SerializedProperty.propertyPath"/> ends with an <c>Array.data[i]</c>
        /// segment, e.g. <c>_slots.Array.data[0]</c>.
        /// </summary>
        /// <remarks>
        /// Not to be confused with <see cref="SerializedProperty.isArray"/>, which is true for the
        /// collection itself (<c>_slots</c>), not for a single element inside it.
        /// </remarks>
        public static bool IsArrayElement(this SerializedProperty property) =>
            property.propertyPath.EndsWith("]", StringComparison.Ordinal);

        /// <summary>
        /// Folds Unity's array-element notation in a property path — <c>"_slots.Array.data[0]._weapon"</c>
        /// becomes <c>"_slots[0]._weapon"</c> — so a '.'-split yields one segment per field.
        /// </summary>
        internal static string SimplifyPropertyPath(string propertyPath) =>
            propertyPath.Replace(".Array.data[", "[");

        /// <inheritdoc cref="SimplifyPropertyPath(string)"/>
        internal static string SimplifyPropertyPath(this SerializedProperty property) =>
            SimplifyPropertyPath(property.propertyPath);
    }
}
