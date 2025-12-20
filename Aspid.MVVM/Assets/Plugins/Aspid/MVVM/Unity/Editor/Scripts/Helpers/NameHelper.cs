using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    // TODO Aspid.MVVM Unity – Write summary
    public static class NameHelper
    {
        private const BindingFlags BackingFieldBindingFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;
        
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string RemovePrefix(this FieldInfo field) =>
            field.Name.RemovePrefix();
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static string RemovePrefix(this string fieldName) =>
            fieldName.Remove(0, GetPrefixCount(fieldName));
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetPrefixCount(this FieldInfo field)
            => GetPrefixCount(field.Name);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetPrefixCount(this string name)
        {
            if (name.StartsWith("__") || name.StartsWith("m_")) return 2;
            return name.StartsWith("_") ? 1 : 0;
        }
        
        /// <summary>
        /// Tries to find a backing field name for a property.
        /// Checks for auto-property backing field and common naming conventions.
        /// </summary>
        /// <param name="property">The property to find a backing field for.</param>
        /// <param name="declaringType">The type that declares the property.</param>
        /// <param name="backingFieldName">The name of the backing field if found.</param>
        /// <returns>True if the backing field was found, false otherwise.</returns>
        public static bool TryGetBackingFieldName(PropertyInfo property, Type declaringType, out string backingFieldName)
        {
            backingFieldName = null;
            if (property is null || declaringType is null) return false;

            var propertyName = property.Name;
            var fields = declaringType.GetFieldInfosIncludingBaseClasses(BackingFieldBindingFlags).ToArray();
            
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
        /// Checks if a property is an auto-property (has compiler-generated backing field).
        /// </summary>
        /// <param name="property">The property to check.</param>
        /// <param name="declaringType">The type that declares the property.</param>
        /// <returns>True if the property is an auto-property, false otherwise.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAutoProperty(PropertyInfo property, Type declaringType)
        {
            if (property is null || declaringType is null) return false;
            
            var autoBackingFieldName = GetAutoPropertyBackingFieldName(property.Name);
            var fields = declaringType.GetFieldInfosIncludingBaseClasses(BackingFieldBindingFlags);
            
            return fields.Any(f => f.Name == autoBackingFieldName);
        }
        
        /// <summary>
        /// Gets the auto-property backing field name.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        /// <returns>The backing field name in format &lt;PropertyName&gt;k__BackingField.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetAutoPropertyBackingFieldName(string propertyName) =>
            $"<{propertyName}>k__BackingField";
    }
}