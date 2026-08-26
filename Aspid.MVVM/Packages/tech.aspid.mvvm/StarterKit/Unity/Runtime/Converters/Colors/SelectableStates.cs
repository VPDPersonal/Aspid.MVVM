#nullable enable
using System;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Which states of a <see cref="ColorBlock"/> a converter writes.
    /// </summary>
    /// <remarks>
    /// The five colors a <see cref="Selectable"/> keeps, as a mask.
    /// </remarks>
    [Flags]
    public enum SelectableStates
    {
        /// <summary>
        /// No state.
        /// </summary>
        None = 0,

        /// <summary>
        /// The resting color.
        /// </summary>
        Normal = 1,

        /// <summary>
        /// The color under the pointer or the focus.
        /// </summary>
        Highlighted = 2,

        /// <summary>
        /// The color while held down.
        /// </summary>
        Pressed = 4,

        /// <summary>
        /// The color once chosen.
        /// </summary>
        Selected = 8,

        /// <summary>
        /// The color while the control is not interactable.
        /// </summary>
        Disabled = 16,

        /// <summary>
        /// Every state but <see cref="SelectableStates.Disabled"/>.
        /// </summary>
        Interactive = Normal | Highlighted | Pressed | Selected,

        /// <summary>
        /// Every state.
        /// </summary>
        All = Normal | Highlighted | Pressed | Selected | Disabled,
    }
}
