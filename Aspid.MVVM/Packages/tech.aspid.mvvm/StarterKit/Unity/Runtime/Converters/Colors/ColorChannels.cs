#nullable enable
using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Which channels of a color a converter writes.
    /// </summary>
    [Flags]
    public enum ColorChannels
    {
        /// <summary>
        /// No channel.
        /// </summary>
        None = 0,

        /// <summary>
        /// The red channel.
        /// </summary>
        R = 1,

        /// <summary>
        /// The green channel.
        /// </summary>
        G = 2,

        /// <summary>
        /// The blue channel.
        /// </summary>
        B = 4,

        /// <summary>
        /// The alpha channel.
        /// </summary>
        A = 8,

        /// <summary>
        /// The three color channels, leaving the alpha alone.
        /// </summary>
        Rgb = R | G | B,

        /// <summary>
        /// Every channel.
        /// </summary>
        All = R | G | B | A,
    }
}
