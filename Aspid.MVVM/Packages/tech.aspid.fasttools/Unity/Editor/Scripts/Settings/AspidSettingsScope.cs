using System;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Editors
{
    /// <summary>
    /// Which storage scopes a settings surface renders. The window's Settings tab shows <see cref="All"/> as the one
    /// full overview; the Unity-native pages each take the scope that matches their convention — the Preferences page
    /// <see cref="User"/>, the Project Settings page <see cref="Shared"/> — so no page lists a control it doesn't own.
    /// </summary>
    [Flags]
    internal enum AspidSettingsScope
    {
        /// <summary>
        /// Team-wide settings persisted in the committed ProjectSettings asset.
        /// </summary>
        Shared = 1 << 0,

        /// <summary>
        /// Per-user settings persisted locally (EditorPrefs), never committed.
        /// </summary>
        User = 1 << 1,

        /// <summary>
        /// Both scopes — the window tab's full overview.
        /// </summary>
        All = Shared | User,
    }
}
