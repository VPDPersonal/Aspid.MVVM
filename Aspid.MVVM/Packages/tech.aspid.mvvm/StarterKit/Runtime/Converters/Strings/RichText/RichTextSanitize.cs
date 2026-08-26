// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// What <see cref="RichTextSanitizeConverter"/> does with markup it will not let through.
    /// </summary>
    /// <remarks>
    /// Append new members rather than inserting one — the order is the serialized value.
    /// </remarks>
    public enum RichTextSanitize
    {
        /// <summary>
        /// Remove the tag and keep the surrounding text.
        /// </summary>
        Strip,

        /// <summary>
        /// Keep the tag but show it as text, by wrapping it in <c>&lt;noparse&gt;</c>.
        /// </summary>
        Escape,
    }
}
