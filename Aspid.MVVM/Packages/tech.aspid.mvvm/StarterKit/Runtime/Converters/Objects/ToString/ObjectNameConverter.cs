#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Reads the name of a Unity object.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Object/To String",
        Name = "Object Name",
        Tooltip = "Reads the name of a Unity object")]
    public sealed class ObjectNameConverter : IConverter<Object?, string>
    {
        private const string CloneSuffix = "(Clone)";

        [Tooltip("Drop the \"(Clone)\" an instantiated object carries.")]
        [SerializeField] private bool _stripCloneSuffix = true;

        [Tooltip("Shown when the object is missing.")]
        [SerializeField] private string _fallback = string.Empty;

        /// <remarks>Default: an empty name for a missing object, with the "(Clone)" suffix dropped.</remarks>
        public ObjectNameConverter() { }

        /// <param name="stripCloneSuffix">Whether to drop the "(Clone)" suffix.</param>
        /// <param name="fallback">
        /// Shown when the object is missing, or <see langword="null"/> to show nothing.
        /// </param>
        public ObjectNameConverter(
            bool stripCloneSuffix = true,
            string? fallback = null)
        {
            _stripCloneSuffix = stripCloneSuffix;
            _fallback = fallback ?? string.Empty;
        }

        /// <summary>
        /// Reads the name of the specified object.
        /// </summary>
        /// <param name="value">The object to name.</param>
        /// <returns>Its name, or the fallback when it is missing or destroyed.</returns>
        public string Convert(Object? value)
        {
            if (value == null) return _fallback;

            var name = value.name;
            if (!_stripCloneSuffix) return name;

            return name.EndsWith(CloneSuffix, StringComparison.Ordinal)
                ? name[..^CloneSuffix.Length].TrimEnd()
                : name;
        }
    }
}
