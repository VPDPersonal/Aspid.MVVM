// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Which culture a converter formats and parses with.
    /// </summary>
    /// <remarks>
    /// A serializable stand-in for <see cref="System.Globalization.CultureInfo"/>, which Unity cannot
    /// serialize — the Inspector picks the mode and <see cref="ToCultureStringExtensions.ToCultureInfo"/>
    /// resolves it at call time, so a change of locale is picked up without re-authoring the scene.
    /// <para>
    /// The choice matters more than it looks: a decimal separator is a comma in half of Europe, so a
    /// number written by one culture and parsed by another loses its fractional part rather than
    /// failing. Text a player sees wants <see cref="CurrentCulture"/>; text that round-trips through
    /// a save file, a network message or <c>PlayerPrefs</c> wants <see cref="InvariantCulture"/>.
    /// </para>
    /// <para>
    /// New members are appended rather than inserted: the order is the serialized value, so moving one
    /// silently rewrites every converter already authored in a scene.
    /// </para>
    /// </remarks>
    public enum CultureInfoMode
    {
        /// <summary>The thread's culture — what the player's machine is set to. The default.</summary>
        CurrentCulture,

        /// <summary>The thread's UI culture, used for resource lookup rather than for formatting.</summary>
        CurrentUICulture,

        /// <summary>Culture-independent. The choice for anything stored, sent, or parsed back.</summary>
        InvariantCulture,

        /// <summary>The culture the operating system itself was installed with.</summary>
        InstalledUICulture,

        /// <summary>
        /// The process-wide default culture. Falls back to <see cref="CurrentCulture"/> while unset,
        /// which is its state unless the application assigns it.
        /// </summary>
        DefaultThreadCurrentCulture,

        /// <summary>
        /// The process-wide default UI culture. Falls back to <see cref="CurrentUICulture"/> while
        /// unset, which is its state unless the application assigns it.
        /// </summary>
        DefaultThreadCurrentUICulture,
    }
}
