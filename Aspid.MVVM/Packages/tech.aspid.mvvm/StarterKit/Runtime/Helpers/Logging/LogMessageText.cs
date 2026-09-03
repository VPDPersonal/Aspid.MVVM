using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Writes types and values the way they read inside a logged message; shared by binders and converters.
    /// </summary>
    public static class LogMessageText
    {
        private static readonly Dictionary<Type, string> _keywords = new()
        {
            [typeof(bool)] = "bool",
            [typeof(byte)] = "byte",
            [typeof(sbyte)] = "sbyte",
            [typeof(char)] = "char",
            [typeof(decimal)] = "decimal",
            [typeof(double)] = "double",
            [typeof(float)] = "float",
            [typeof(int)] = "int",
            [typeof(uint)] = "uint",
            [typeof(long)] = "long",
            [typeof(ulong)] = "ulong",
            [typeof(short)] = "short",
            [typeof(ushort)] = "ushort",
            [typeof(object)] = "object",
            [typeof(string)] = "string",
        };

        /// <summary>
        /// Writes a type name the way it reads in code: <c>BoolToValueConverter&lt;float&gt;</c>, not <c>BoolToValueConverter`1</c>.
        /// </summary>
        /// <param name="type">The type to name.</param>
        /// <returns>The readable name.</returns>
        public static string GetTypeName(this Type type)
        {
            if (_keywords.TryGetValue(type, out var keyword)) return keyword;
            if (!type.IsGenericType) return type.Name;

            var name = type.Name;
            var tick = name.IndexOf('`');
            if (tick >= 0) name = name[..tick];

            return $"{name}<{string.Join(", ", Array.ConvertAll(type.GetGenericArguments(), GetTypeName))}>";
        }

        /// <summary>
        /// Writes what was needed and what came instead:
        /// <c>expected a whole number but got "abc"</c>.
        /// </summary>
        /// <param name="value">The value that would not convert.</param>
        /// <param name="expected">What was needed, as a noun phrase: "a whole number".</param>
        /// <returns>The problem, as a sentence without the trailing period.</returns>
        public static string Expected(this object? value, string expected) =>
            $"expected {expected} but got {value.Describe()}";

        /// <summary>
        /// Writes a value unambiguously: <see langword="null"/> as the word "null", a string in
        /// double quotes, a char in single quotes, and everything else as it prints itself.
        /// </summary>
        /// <param name="value">The value to describe.</param>
        /// <returns>The readable description.</returns>
        public static string Describe(this object? value) => value switch
        {
            null => "null",
            string text => $"\"{text}\"",
            char symbol => $"'{symbol}'",
            _ => $"{value}",
        };
    }
}
