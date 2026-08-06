using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.SerializeReferences.Editors
{
    /// <summary>
    /// Editor-session clipboard backing the Copy/Paste context-menu entries of the
    /// <c>[TypeSelector]</c> drawer on <c>[SerializeReference]</c> fields. Stores the copied managed-reference value as JSON plus its
    /// concrete <see cref="Type"/>, so a paste reconstructs an independent instance (rather than aliasing the
    /// source object) and survives across different fields, inspectors, and target objects within the session.
    /// </summary>
    internal static class SerializeReferenceClipboard
    {
        private static bool _hasContent;
        private static string _json;

        /// <summary>
        /// The concrete type of the copied value, or <see langword="null"/> when an empty reference was copied.
        /// </summary>
        public static Type Type { get; private set; }

        /// <summary>
        /// Captures <paramref name="value"/> into the clipboard. Copying <see langword="null"/> is meaningful — a
        /// subsequent paste clears the target field.
        /// </summary>
        public static void Copy(object value)
        {
            _hasContent = true;
            Type = value?.GetType();
            _json = value is null ? null : JsonUtility.ToJson(value);
        }

        /// <summary>
        /// Returns <see langword="true"/> when the clipboard holds content that can be pasted into a field whose
        /// declared managed-reference type is <paramref name="fieldType"/> (an empty reference always pastes —
        /// it clears the field). The optional <paramref name="filter"/> applies the same <c>[TypeSelector]</c>
        /// base-type narrowing the picker, drag-drop and Smart-Fix enforce, so paste cannot assign a type the
        /// dropdown would hide.
        /// </summary>
        public static bool CanPasteInto(Type fieldType, Func<Type, bool> filter = null)
        {
            if (!_hasContent) return false;
            if (Type is null) return true;
            if (fieldType is not null && !fieldType.IsAssignableFrom(Type)) return false;
            return filter is null || filter(Type);
        }

        /// <summary>
        /// Reconstructs a fresh instance from the clipboard contents for assignment to a managed reference, or
        /// <see langword="null"/> when an empty reference was copied. The result is independent of the copied object.
        /// </summary>
        public static object CreateInstance()
        {
            if (!_hasContent || Type is null) return null;

            return string.IsNullOrEmpty(_json)
                ? SerializeReferenceHelpers.CreateInstance(Type)
                : JsonUtility.FromJson(_json, Type);
        }
    }
}
