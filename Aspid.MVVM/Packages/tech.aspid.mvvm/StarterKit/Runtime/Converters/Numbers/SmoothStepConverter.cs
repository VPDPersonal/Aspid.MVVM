using Aspid.FastTools.Types;
using System;
using UnityEngine;

// The named converter aliases are [Obsolete]. The converters below keep implementing them for
// one release so that a [SerializeReference] field a project declares as one still
// deserializes; the base lists go with the aliases in the next major.
#pragma warning disable CS0618 // Type or member is obsolete

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Eases a value between two bounds with <see cref="Mathf.SmoothStep"/>.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Number", Name = "Smooth Step", Tooltip = "Eases a value between two bounds with smoothstep")]
    public sealed class SmoothStepConverter : IConverterFloat
    {
        [Tooltip("The value that maps to 0.")]
        [SerializeField] private float _from;

        [Tooltip("The value that maps to 1.")]
        [SerializeField] private float _to = 1f;

        /// <remarks>Default: over 0..1.</remarks>
        public SmoothStepConverter() { }

        /// <param name="from">The value that maps to 0.</param>
        /// <param name="to">The value that maps to 1.</param>
        public SmoothStepConverter(float from, float to)
        {
            _from = from;
            _to = to;
        }

        /// <summary>
        /// Eases the specified value.
        /// </summary>
        /// <param name="value">The value to ease.</param>
        /// <returns>The eased value.</returns>
        public float Convert(float value) => Mathf.SmoothStep(_from, _to, value);
    }
}
