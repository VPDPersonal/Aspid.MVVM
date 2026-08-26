// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Which culture a value is formatted and parsed with.
    /// </summary>
    /// <remarks>
    /// A serializable stand-in for <see cref="System.Globalization.CultureInfo"/>, which Unity cannot
    /// serialize; <see cref="ToCultureStringExtensions.ToCultureInfo"/> resolves it at call time.
    /// A decimal separator is a comma in half of Europe, so a number written by one culture and parsed
    /// by another loses its fractional part rather than failing. Append new members rather than
    /// inserting one — the order is the serialized value.
    /// </remarks>
    public enum CultureInfoMode
    {
        /// <summary>
        /// The thread's culture — what the player's machine is set to.
        /// </summary>
        CurrentCulture,

        /// <summary>
        /// The thread's UI culture, used for resource lookup rather than formatting.
        /// </summary>
        CurrentUICulture,

        /// <summary>
        /// Culture-independent — for anything stored, sent, or parsed back.
        /// </summary>
        InvariantCulture,

        /// <summary>
        /// The culture the operating system was installed with.
        /// </summary>
        InstalledUICulture,

        /// <summary>
        /// The process-wide default culture, falling back to <see cref="CurrentCulture"/> while unset.
        /// </summary>
        DefaultThreadCurrentCulture,

        /// <summary>
        /// The process-wide default UI culture, falling back to <see cref="CurrentUICulture"/> while unset.
        /// </summary>
        DefaultThreadCurrentUICulture,
    }
}
