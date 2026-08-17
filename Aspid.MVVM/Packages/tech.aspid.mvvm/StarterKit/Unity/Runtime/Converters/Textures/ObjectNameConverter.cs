#nullable enable
using Aspid.FastTools.Types;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Reads the name of a Unity object.
    /// </summary>
    /// <remarks>Debug overlays and tooltips that label whatever they are pointed at.</remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Texture", Name = "Object Name", Tooltip = "Reads the name of a Unity object")]
    public sealed class ObjectNameConverter : IConverter<UnityEngine.Object?, string>
    {
        [Tooltip("Shown when the object is missing.")]
        [SerializeField] private string _fallback = string.Empty;

        [Tooltip("Drop the \"(Clone)\" an instantiated object carries.")]
        [SerializeField] private bool _stripCloneSuffix = true;

        public ObjectNameConverter() { }

        /// <param name="fallback">Shown when the object is missing.</param>
        /// <param name="stripCloneSuffix">Whether to drop the "(Clone)" suffix.</param>
        public ObjectNameConverter(string fallback, bool stripCloneSuffix = true)
        {
            _fallback = fallback;
            _stripCloneSuffix = stripCloneSuffix;
        }

        /// <summary>
        /// Reads the name of the specified object.
        /// </summary>
        /// <param name="value">The object to name.</param>
        /// <returns>Its name, or the fallback when it is missing or destroyed.</returns>
        public string Convert(UnityEngine.Object? value)
        {
            // Unity's overloaded == also catches a destroyed object, whose name access would throw.
            if (value == null) return _fallback;

            var name = value.name;
            if (!_stripCloneSuffix) return name;

            const string clone = "(Clone)";
            return name.EndsWith(clone, StringComparison.Ordinal)
                ? name[..^clone.Length].TrimEnd()
                : name;
        }
    }
}
