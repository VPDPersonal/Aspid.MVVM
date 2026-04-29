// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements.Editors.Internal
{
    /// <summary>
    /// Configuration preset for an <see cref="AspidInspectorHeader"/>.
    /// Use the fluent builder methods to create a customized preset.
    /// </summary>
    public struct AspidInspectorHeaderPreset
    {
        /// <summary>
        /// The default preset: empty texts, success status.
        /// </summary>
        public static AspidInspectorHeaderPreset Default => new AspidInspectorHeaderPreset()
            .SetText(string.Empty)
            .SetSubtext(string.Empty)
            .SetStatus(StatusStyle.Type.Success);

        /// <summary>
        /// The primary header text.
        /// </summary>
        public string Text;

        /// <summary>
        /// The secondary (subtext) label beneath the primary text.
        /// </summary>
        public string Subtext;

        /// <summary>
        /// The status color accent of the header.
        /// </summary>
        public StatusStyle.Type Status;

        /// <summary>
        /// Sets <see cref="Text"/> and returns the modified preset.
        /// </summary>
        /// <param name="value">The new primary text.</param>
        public AspidInspectorHeaderPreset SetText(string value)
        {
            Text = value;
            return this;
        }

        /// <summary>
        /// Sets <see cref="Subtext"/> and returns the modified preset.
        /// </summary>
        /// <param name="value">The new subtext.</param>
        public AspidInspectorHeaderPreset SetSubtext(string value)
        {
            Subtext = value;
            return this;
        }

        /// <summary>
        /// Sets <see cref="Status"/> and returns the modified preset.
        /// </summary>
        /// <param name="value">The new status.</param>
        public AspidInspectorHeaderPreset SetStatus(StatusStyle.Type value)
        {
            Status = value;
            return this;
        }
    }
}
