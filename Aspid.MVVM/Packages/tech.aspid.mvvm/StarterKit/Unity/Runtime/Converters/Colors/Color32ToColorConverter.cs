#nullable enable
using Aspid.FastTools.Types;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Widens a <see cref="Color32"/> into a <see cref="Color"/>.
    /// </summary>
    /// <remarks>
    /// A backend that sends colours as four bytes — the compact form in a JSON payload or a save
    /// file — reaching a binder that works in floats. Every binder in the package takes
    /// <see cref="Color"/>, so a ViewModel holding the byte form could not bind one at all.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Colour", Name = "Color32 To Color", Tooltip = "Widens a Color32 into a Color")]
    public sealed class Color32ToColorConverter : IConverter<Color32, Color>
    {
        /// <summary>
        /// Widens the specified byte colour.
        /// </summary>
        /// <param name="value">The byte colour to widen.</param>
        /// <returns>The same colour with each channel as a 0..1 float.</returns>
        public Color Convert(Color32 value) => value;
    }
}
