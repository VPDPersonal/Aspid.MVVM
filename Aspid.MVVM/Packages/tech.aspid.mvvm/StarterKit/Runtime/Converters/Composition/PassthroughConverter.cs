#nullable enable
using System;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Returns its input unchanged.
    /// </summary>
    /// <typeparam name="T">The type of the value passing through.</typeparam>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Composition",
        Name = "Passthrough",
        Tooltip = "Returns its input unchanged")]
    public class PassthroughConverter<T> : ITwoWayConverter<T, T>
    {
        /// <summary>
        /// Returns the specified value unchanged.
        /// </summary>
        /// <param name="value">The value to pass through.</param>
        /// <returns>The same value.</returns>
        public T Convert(T value) => value;

        /// <summary>
        /// Returns the specified value unchanged.
        /// </summary>
        /// <param name="value">The value to pass through.</param>
        /// <returns>The same value.</returns>
        public T ConvertBack(T value) => value;
    }
}
