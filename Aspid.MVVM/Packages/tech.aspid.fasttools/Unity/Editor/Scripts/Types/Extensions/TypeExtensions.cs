using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Types.Editors
{
    /// <summary>
    /// Editor-side extension methods for <see cref="Type"/>: locate the <see cref="MonoScript"/>
    /// asset that defines a type and open it in the external script editor.
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Searches the Asset Database for the <see cref="MonoScript"/> that defines the given type.
        /// </summary>
        /// <remarks>
        /// Falls back to scanning script text when <see cref="MonoScript.GetClass"/> yields no match,
        /// so types whose file name differs from the type name are still found.
        /// </remarks>
        /// <param name="type">The type to locate a script asset for.</param>
        /// <returns>
        /// The matching <see cref="MonoScript"/> asset, or <see langword="null"/> if none is found.
        /// </returns>
        public static MonoScript FindMonoScript(this Type type)
        {
            if (type is null) return null;

            var lookupType = GetLookupType(type);
            var typeNamespace = lookupType.Namespace;
            var typeName = TypeUtility.StripArity(lookupType.Name);

            var scripts = AssetDatabase.FindAssets(filter: $"t:MonoScript {typeName}")
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<MonoScript>)
                .Where(script => script is not null)
                .ToArray();

            var exact = scripts.FirstOrDefault(script => script.GetClass() == lookupType);
            if (exact is not null) return exact;

            var pattern = GetDeclarationPattern(lookupType.IsEnum, typeName);

            foreach (var script in scripts)
            {
                var text = script.text;
                if (string.IsNullOrWhiteSpace(text)) continue;
                if (!string.IsNullOrWhiteSpace(typeNamespace) && !text.Contains($"namespace {typeNamespace}")) continue;
                if (!Regex.IsMatch(text, pattern)) continue;

                return script;
            }

            return null;
        }

        /// <summary>
        /// Opens the script that defines <paramref name="type"/> in the configured external
        /// editor at the line of the type declaration. Logs a warning and is a no-op when
        /// no <see cref="MonoScript"/> can be located; a <see langword="null"/> type is silently ignored.
        /// </summary>
        public static void OpenInScriptEditor(this Type type)
        {
            if (type is null) return;
            var monoScript = type.FindMonoScript();

            if (monoScript is null)
            {
                Debug.LogWarning($"MonoScript for type {type.AssemblyQualifiedName} not found.");
                return;
            }

            AssetDatabase.OpenAsset(monoScript, FindTypeLineNumber(monoScript, type));
        }

        private static int FindTypeLineNumber(MonoScript script, Type type)
        {
            var lookupType = GetLookupType(type);
            return FindTypeLineNumber(script.text, lookupType.IsEnum, TypeUtility.StripArity(lookupType.Name));
        }

        private static int FindTypeLineNumber(string text, bool isEnum, string typeName)
        {
            if (string.IsNullOrWhiteSpace(text)) return 1;

            var pattern = GetDeclarationPattern(isEnum, typeName);
            var lines = text.Split('\n');

            for (var i = 0; i < lines.Length; i++)
            {
                if (Regex.IsMatch(lines[i], pattern))
                    return i + 1;
            }

            return 1;
        }

        private static Type GetLookupType(Type type) =>
            type.IsGenericType ? type.GetGenericTypeDefinition() : type;

        // Enums are matched separately so a class/struct/record lookup never lands on a same-named enum declaration.
        private static string GetDeclarationPattern(bool isEnum, string typeName) => isEnum
            ? $@"\benum\s+{Regex.Escape(typeName)}\b"
            : $@"\b(class|struct|record)\s+{Regex.Escape(typeName)}\b";
    }
}
