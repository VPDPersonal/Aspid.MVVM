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
    /// The five colours a <see cref="Selectable"/> keeps, as a mask. A converter that writes all of
    /// them cannot express "everything except disabled", which is the usual shape of a theme: the
    /// disabled colour says the control is unavailable and must survive the theming that recolours
    /// the rest.
    /// </remarks>
    [Flags]
    public enum SelectableStates
    {
        /// <summary>
        /// No state.
        /// </summary>
        None = 0,

        /// <summary>
        /// The resting colour.
        /// </summary>
        Normal = 1,

        /// <summary>
        /// The colour under the pointer or the focus.
        /// </summary>
        Highlighted = 2,

        /// <summary>
        /// The colour while held down.
        /// </summary>
        Pressed = 4,

        /// <summary>
        /// The colour once chosen.
        /// </summary>
        Selected = 8,

        /// <summary>
        /// The colour while the control is not interactable.
        /// </summary>
        Disabled = 16,

        /// <summary>
        /// Every state but the disabled one, which usually has to stay readable as itself.
        /// </summary>
        Interactive = Normal | Highlighted | Pressed | Selected,

        /// <summary>
        /// Every state.
        /// </summary>
        All = Normal | Highlighted | Pressed | Selected | Disabled,
    }
}
