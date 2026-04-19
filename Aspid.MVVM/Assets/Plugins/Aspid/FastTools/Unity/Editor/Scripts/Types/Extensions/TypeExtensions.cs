using System;
using System.Linq;
using UnityEditor;
using System.Collections.Generic;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Types.Editors
{
    /// <summary>
    /// Editor-side extension methods for <see cref="Type"/> that locate the
    /// <see cref="MonoScript"/> asset corresponding to a given type using the Asset Database.
    /// </summary>
    public static class TypeExtensions
    {
        private static readonly Dictionary<string, Regex> _regexCache = new();

        /// <summary>
        /// Searches the Asset Database for the <see cref="MonoScript"/> that defines the given type.
        /// The search first tries an exact match via <see cref="MonoScript.GetClass"/>, then falls back
        /// to a regex match against the script text, checking the namespace and the type declaration keyword
        /// (<c>class</c>, <c>struct</c>, <c>record</c>, or <c>enum</c>).
        /// </summary>
        /// <param name="type">The type to locate a script asset for.</param>
        /// <returns>
        /// The matching <see cref="MonoScript"/> asset, or <see langword="null"/> if none is found.
        /// </returns>
        public static MonoScript FindMonoScript(this Type type)
        {
            var isEnum = type.IsEnum;
            var typeName = type.Name;
            var typeNamespace = type.Namespace;

            var scripts = AssetDatabase.FindAssets(filter: $"t:MonoScript {typeName}")
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<MonoScript>)
                .Where(script => script is not null)
                .ToArray();

            var regex = GetRegex(isEnum, typeName);

            foreach (var script in scripts)
            {
                if (script.GetClass() != type) continue;
                return script;
            }

            foreach (var script in scripts)
            {
                var text = script.text;
                if (string.IsNullOrWhiteSpace(text)) continue;
                if (!string.IsNullOrWhiteSpace(typeNamespace) && !text.Contains($"namespace {typeNamespace}")) continue;
                if (!regex.IsMatch(text)) continue;

                return script;
            }

            return null;
        }

        /// <summary>
        /// Searches the Asset Database for the <see cref="MonoScript"/> that defines the given type
        /// and also determines the 1-based line number of the type declaration within that script.
        /// </summary>
        /// <param name="type">The type to locate.</param>
        /// <returns>
        /// A tuple of the matched <see cref="MonoScript"/> and the 1-based line number of the type declaration.
        /// If no script is found, returns <c>(null, 0)</c>.
        /// </returns>
        public static (MonoScript script, int line) FindMonoScriptWithLine(this Type type)
        {
            var script = type.FindMonoScript();
            if (script is null) return (script: null, line: 0);

            var line = FindTypeLineNumber(script.text, type.Name, type.IsEnum);
            return (script, line);
        }
        
        private static int FindTypeLineNumber(string text, string typeName, bool isEnum)
        {
            if (string.IsNullOrEmpty(text)) return 1;

            var regex = GetRegex(isEnum, typeName);
            var lines = text.Split('\n');

            for (var i = 0; i < lines.Length; i++)
            {
                if (regex.IsMatch(lines[i]))
                    return i + 1;
            }

            return 1;
        }
        
        private static string GetPattern(bool isEnum, string typeName) => isEnum
            ? $@"\benum\s+{Regex.Escape(typeName)}\b"
            : $@"\b(class|struct|record)\s+{Regex.Escape(typeName)}\b";
        
        private static Regex GetRegex(bool isEnum, string typeName)
        {
            var key = $"{isEnum}:{typeName}";
            if (_regexCache.TryGetValue(key, out var cached))
                return cached;

            var regex = new Regex(GetPattern(isEnum, typeName), RegexOptions.Compiled);
            _regexCache[key] = regex;
            return regex;
        }
    }
}
