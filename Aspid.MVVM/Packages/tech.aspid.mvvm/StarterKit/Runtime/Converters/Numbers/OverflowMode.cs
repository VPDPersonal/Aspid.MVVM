#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// What <see cref="NumericCastConverter"/> does with a number the target type cannot hold.
    /// </summary>
    /// <remarks>
    /// Narrowing is the one numeric conversion with no obviously right answer, so the answer is
    /// authored rather than assumed.
    /// </remarks>
    public enum OverflowMode
    {
        /// <summary>
        /// Return the nearest value the target type can hold: too large becomes its maximum, too
        /// small its minimum, and a NaN becomes zero on an integer target. NaN and the infinities
        /// survive a double-to-float narrowing, which can represent them.
        /// </summary>
        /// <remarks>
        /// Deliberately the zero value. A <c>[SerializeReference]</c> instance restored from a scene
        /// written before this field existed reads whatever sits at zero, and that default must be
        /// the one that loses the least — not the one that silently turns a large number negative.
        /// </remarks>
        Saturate,

        /// <summary>
        /// Convert the way a plain <c>(int)</c> cast does. An integer that does not fit keeps its low
        /// bits and can change sign; a floating-point value outside the target's range produces a
        /// result the C# specification leaves undefined, so it may differ between platforms.
        /// </summary>
        Unchecked,

        /// <summary>
        /// Throw an <see cref="OverflowException"/> for a value that does not fit. The throw happens
        /// inside a binder's value push, so read the note on
        /// <see cref="ConverterFailureMode.Throw"/> before choosing it.
        /// </summary>
        /// <remarks>
        /// This is about a value too LARGE for the target. A double too small for a float still
        /// becomes zero without a word — that is underflow, and no mode here reports it.
        /// </remarks>
        Checked,
    }
}
