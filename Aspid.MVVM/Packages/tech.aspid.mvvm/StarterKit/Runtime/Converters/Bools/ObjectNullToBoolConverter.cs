using Aspid.FastTools.Types;
using System;

// The named converter aliases are [Obsolete]. The converters below keep implementing them for
// one release so that a [SerializeReference] field a project declares as one still
// deserializes; the base lists go with the aliases in the next major.
#pragma warning disable CS0618 // Type or member is obsolete

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Converts object references to boolean based on null check, with optional inversion.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Bool", Name = "Object Null To Bool", Tooltip = "Converts object references to boolean based on null check, with optional inversion")]
    public class ObjectNullToBoolConverter : IConverterObjectToBool
    {
        [UnityEngine.Tooltip("Invert the result — true when the object is not null.")]
        [UnityEngine.SerializeField]
        private bool _isInvert;

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectNullToBoolConverter"/> class.
        /// </summary>
        public ObjectNullToBoolConverter()
            : this(isInvert: false) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectNullToBoolConverter"/> class.
        /// </summary>
        /// <param name="isInvert">If <see langword="true"/>, inverts the result of the null check. Default is <see langword="false"/>.</param>
        public ObjectNullToBoolConverter(bool isInvert)
        {
            _isInvert = isInvert;
        }

        /// <summary>
        /// Converts an object to boolean based on whether it is null.
        /// </summary>
        /// <param name="value">The object to check.</param>
        /// <returns><see langword="true"/> if the value is null (or not null if inverted), otherwise <see langword="false"/>.</returns>
        public bool Convert(object? value)
        {
            var isNull = value is null;
            return _isInvert ? !isNull : isNull;
        }
    }
}