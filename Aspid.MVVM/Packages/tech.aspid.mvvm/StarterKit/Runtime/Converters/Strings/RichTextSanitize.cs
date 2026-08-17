#nullable enable

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// What <see cref="SanitizeRichTextConverter"/> does with markup it will not let through.
    /// </summary>
    /// <remarks>
    /// New members are appended rather than inserted: the order is the serialized value, so moving
    /// one silently rewrites every converter already authored in a scene.
    /// </remarks>
    public enum RichTextSanitize
    {
        /// <summary>
        /// Remove the tag and keep the text around it.
        /// </summary>
        Strip,

        /// <summary>
        /// Keep the tag but show it as the characters it is made of, by wrapping it in
        /// <c>&lt;noparse&gt;</c>.
        /// </summary>
        Escape,
    }
}
