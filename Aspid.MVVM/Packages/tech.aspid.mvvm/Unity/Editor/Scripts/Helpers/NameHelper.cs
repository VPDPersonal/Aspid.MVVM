using System;
using System.Linq;
using Aspid.FastTools;
using System.Reflection;
using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Provides utility methods for generated property and backing field names.
    /// </summary>
    public static class NameHelper
    {
        private const BindingFlags BackingFieldBindingFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;
        
        /// <summary>
        /// Derives the generated property name from a field name.
        /// </summary>
        /// <param name="field">The field to inspect.</param>
        /// <returns>The capitalized name without its field prefix.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetGeneratedPropertyName(this FieldInfo field)
            => GetGeneratedPropertyName(field.Name);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static string GetGeneratedPropertyName(this string fieldName)
        {
            fieldName = fieldName.Remove(0, GetPrefixCount(fieldName));
            
            var firstSymbol = fieldName[0];
            if (char.IsLower(firstSymbol))
            {
                fieldName = fieldName.Remove(0, 1);
                fieldName = char.ToUpper(firstSymbol) + fieldName;
            }

            return fieldName;
        }

        /// <summary>
        /// Removes the naming prefix from a field name.
        /// </summary>
        /// <param name="field">The field to inspect.</param>
        /// <returns>The field name without its prefix.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string RemovePrefix(this FieldInfo field) =>
            field.Name.RemovePrefix();
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static string RemovePrefix(this string fieldName) =>
            fieldName.Remove(0, GetPrefixCount(fieldName));
        
        /// <summary>
        /// Returns the length of a field name prefix.
        /// </summary>
        /// <param name="field">The field to inspect.</param>
        /// <returns>Two for <c>__</c> or <c>m_</c>, one for <c>_</c>; otherwise, zero.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetPrefixCount(this FieldInfo field)
            => GetPrefixCount(field.Name);
        
        /// <summary>
        /// Returns the length of a field name prefix.
        /// </summary>
        /// <param name="name">The field name.</param>
        /// <returns>Two for <c>__</c> or <c>m_</c>, one for <c>_</c>; otherwise, zero.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetPrefixCount(this string name)
        {
            if (name.StartsWith("__") || name.StartsWith("m_")) return 2;
            return name.StartsWith("_") ? 1 : 0;
        }
        
        /// <summary>
        /// Finds a backing field by compiler naming or common field prefixes.
        /// </summary>
        /// <param name="property">The property to inspect.</param>
        /// <param name="declaringType">The type to search, including its base types.</param>
        /// <param name="backingFieldName">The matching field name; <see langword="null"/> when none is found.</param>
        /// <returns><see langword="true"/> if a matching field is found; otherwise, <see langword="false"/>.</returns>
        public static bool TryGetBackingFieldName(PropertyInfo property, Type declaringType, out string backingFieldName)
        {
            backingFieldName = null;
            if (property is null || declaringType is null) return false;

            var propertyName = property.Name;
            var fields = declaringType
                .GetMembersInfosIncludingBaseClasses(BackingFieldBindingFlags)
                .OfType<FieldInfo>()
                .ToArray();
            
            // Check for the auto-property backing field: <PropertyName>k__BackingField
            var autoBackingFieldName = $"<{propertyName}>k__BackingField";
            if (fields.Any(f => f.Name == autoBackingFieldName))
            {
                backingFieldName = autoBackingFieldName;
                return true;
            }
            
            // Check common naming conventions
            var lowerFirstChar = char.ToLower(propertyName[0]) + propertyName[1..];
            var possibleNames = new[]
            {
                $"_{lowerFirstChar}",       // _propertyName
                $"_{propertyName}",         // _PropertyName
                lowerFirstChar,             // propertyName
                $"m_{lowerFirstChar}",      // m_propertyName
            };

            foreach (var possibleName in possibleNames)
            {
                if (fields.Any(field => field.Name == possibleName))
                {
                    backingFieldName = possibleName;
                    return true;
                }
            }

            return false;
        }
        
        /// <summary>
        /// Determines whether a compiler-named backing field exists.
        /// </summary>
        /// <param name="property">The property to inspect.</param>
        /// <param name="declaringType">The type to search, including its base types.</param>
        /// <returns><see langword="true"/> if the backing field exists; otherwise, <see langword="false"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAutoProperty(PropertyInfo property, Type declaringType)
        {
            if (property is null || declaringType is null) return false;
            
            var autoBackingFieldName = GetAutoPropertyBackingFieldName(property.Name);
            var fields = declaringType.GetMembersInfosIncludingBaseClasses(BackingFieldBindingFlags).OfType<FieldInfo>();
            
            return fields.Any(f => f.Name == autoBackingFieldName);
        }
        
        /// <summary>
        /// Returns the compiler-generated backing field name.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        /// <returns>The name in the form <c>&lt;PropertyName&gt;k__BackingField</c>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetAutoPropertyBackingFieldName(string propertyName) =>
            $"<{propertyName}>k__BackingField";
    }
}