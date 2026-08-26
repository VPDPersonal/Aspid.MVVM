#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Which sides of a <see cref="RectOffset"/> a converter writes.
    /// </summary>
    [Flags]
    public enum RectSides
    {
        /// <summary>
        /// No side.
        /// </summary>
        None = 0,

        /// <summary>
        /// The left side.
        /// </summary>
        Left = 1,

        /// <summary>
        /// The right side.
        /// </summary>
        Right = 2,

        /// <summary>
        /// The top side.
        /// </summary>
        Top = 4,

        /// <summary>
        /// The bottom side.
        /// </summary>
        Bottom = 8,

        /// <summary>
        /// Left and right.
        /// </summary>
        Horizontal = Left | Right,

        /// <summary>
        /// Top and bottom.
        /// </summary>
        Vertical = Top | Bottom,

        /// <summary>
        /// Every side.
        /// </summary>
        All = Left | Right | Top | Bottom,
    }
}
