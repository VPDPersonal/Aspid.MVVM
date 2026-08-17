#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// One colour of a <see cref="ThresholdRichTextColorConverter"/> scale.
    /// </summary>
    [Serializable]
    public struct ColorStop
    {
        /// <summary>
        /// The value at or above which this colour applies.
        /// </summary>
        [Tooltip("The value at or above which this colour applies.")]
        public float Threshold;

        /// <summary>
        /// The colour used from this threshold up.
        /// </summary>
        [Tooltip("The colour used from this threshold up.")]
        public Color Color;
    }
}
