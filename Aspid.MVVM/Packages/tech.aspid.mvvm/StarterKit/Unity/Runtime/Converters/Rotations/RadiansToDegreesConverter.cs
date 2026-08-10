#nullable enable
using Aspid.FastTools.Types;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Converts between radians and degrees.
    /// </summary>
    /// <remarks>
    /// The same arithmetic as <see cref="DegreesToRadiansConverter"/> the other way round, and a
    /// class of its own rather than that one's <c>ConvertBack</c> because a
    /// <see cref="BindMode.OneWay"/> binding only ever calls <c>Convert</c> — a source that already
    /// holds radians had no entry in the picker that would turn them into degrees.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Rotation", Name = "Radians To Degrees", Tooltip = "Converts between radians and degrees")]
    public sealed class RadiansToDegreesConverter : ITwoWayConverter<float, float>
    {
        /// <summary>
        /// Converts the specified angle to degrees.
        /// </summary>
        /// <param name="value">The angle, in radians.</param>
        /// <returns>The angle, in degrees.</returns>
        public float Convert(float value) => value * Mathf.Rad2Deg;

        /// <summary>
        /// Converts the specified angle to radians.
        /// </summary>
        /// <param name="value">The angle, in degrees.</param>
        /// <returns>The angle, in radians.</returns>
        public float ConvertBack(float value) => value * Mathf.Deg2Rad;
    }
}
