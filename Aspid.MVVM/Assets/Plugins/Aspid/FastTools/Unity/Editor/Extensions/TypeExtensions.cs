using System;
using UnityEditor;
using UnityEngine;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Editors
{
    public static class TypeExtensions
    {
        public static (MonoScript script, int lineNumber) GetMonoScriptFromType(this Type type)
        {
            var isEnum = type.IsEnum;
            var typeName = type.Name;
            var typeNamespace = type.Namespace;
            var scripts = Resources.FindObjectsOfTypeAll<MonoScript>();

            var regex = new Regex(GetPattern(isEnum, typeName));
            
            foreach (var script in scripts)
            {
                if (script.GetClass() != type) continue;
                
                var line = FindTypeLineNumber(script.text, typeName, isEnum);
                return (script, line);
            }
            
            foreach (var script in scripts)
            {
                var text = script.text;
                if (string.IsNullOrWhiteSpace(text)) continue;
                if (!string.IsNullOrWhiteSpace(typeNamespace) && !text.Contains($"namespace {typeNamespace}")) continue;
                if (!regex.IsMatch(text)) continue;

                var line = FindTypeLineNumber(text, typeName, isEnum);
                return (script, line);
            }
            
            return (script: null, lineNumber: 1);
        }
        
        private static int FindTypeLineNumber(string text, string typeName, bool isEnum)
        {
            if (string.IsNullOrEmpty(text)) return 1;
            
            var regex = new Regex(GetPattern(isEnum, typeName));
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
    }
}
