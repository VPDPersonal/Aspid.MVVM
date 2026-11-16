#nullable enable
using System;
using System.Reflection;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Provides extension methods for <see cref="FieldInfo"/>.
    /// </summary>
    public static class FieldInfoExtensions
    {
        /// <summary>
        /// Assigns an array converted to the field type, or its first element for a scalar field.
        /// </summary>
        /// <typeparam name="T">The input value type.</typeparam>
        /// <param name="field">The field to write.</param>
        /// <param name="obj">The object containing the field.</param>
        /// <param name="value">The values to assign; <see langword="null"/> or empty clears the field.</param>
        public static void SetValueFromCastValue<T>(this FieldInfo field, object obj, params T?[]? value)
        {
            if (value is null || value.Length is 0)
            {
                field.SetValue(obj, null);
                return;
            }
            
            var isArray = field.FieldType.IsArray;
            var type = isArray ? field.FieldType.GetElementType()! : field.FieldType;
            
            var typedArray = Array.CreateInstance(type, value.Length);
            Array.Copy(value, typedArray, value.Length);
            
            field.SetValue(obj, isArray ? typedArray : typedArray.GetValue(0));
        }

        /// <summary>
        /// Reads a scalar or array field as an array.
        /// </summary>
        /// <typeparam name="T">The expected element type.</typeparam>
        /// <param name="field">The field to read.</param>
        /// <param name="obj">The object containing the field.</param>
        /// <returns>The matching values; an empty array for a missing or incompatible value.</returns>
        public static T?[] GetValueAsArray<T>(this FieldInfo field, object obj)
        {
            T?[]? values;
                
            if (field.FieldType.IsArray) values = field.GetValue(obj) as T[];
            else values = field.GetValue(obj) is T binder ? new[] { binder } : null;

            return values ?? Array.Empty<T>();
        }
    }
}