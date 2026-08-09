using System;
using System.Globalization;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Turns a <see cref="CultureInfoMode"/> into the culture it names, and formats numbers with it.
    /// </summary>
    public static class ToCultureStringExtensions
    {
        /// <summary>
        /// Resolves the culture a <see cref="CultureInfoMode"/> names.
        /// </summary>
        /// <param name="mode">The mode to resolve.</param>
        /// <returns>The named culture, never <see langword="null"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="mode"/> is not a declared value.</exception>
        /// <remarks>
        /// Both <c>DefaultThread…</c> statics are <see langword="null"/> until an application sets
        /// them, which is the usual state — those two entries in the Inspector dropdown would
        /// otherwise do nothing and hand a <see langword="null"/> culture to the caller. They fall
        /// back to the corresponding current culture instead.
        /// </remarks>
        public static CultureInfo ToCultureInfo(this CultureInfoMode mode) => mode switch
        {
            CultureInfoMode.CurrentCulture => CultureInfo.CurrentCulture,
            CultureInfoMode.CurrentUICulture => CultureInfo.CurrentUICulture,
            CultureInfoMode.InvariantCulture => CultureInfo.InvariantCulture,
            CultureInfoMode.InstalledUICulture => CultureInfo.InstalledUICulture,
            CultureInfoMode.DefaultThreadCurrentCulture => CultureInfo.DefaultThreadCurrentCulture ?? CultureInfo.CurrentCulture,
            CultureInfoMode.DefaultThreadCurrentUICulture => CultureInfo.DefaultThreadCurrentUICulture ?? CultureInfo.CurrentUICulture,
            _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, null)
        };

        public static string ToCultureString(this int number, CultureInfoMode mode) =>
            number.ToString(mode.ToCultureInfo());

        public static string ToCultureString(this uint number, CultureInfoMode mode) =>
            number.ToString(mode.ToCultureInfo());

        public static string ToCultureString(this long number, CultureInfoMode mode) =>
            number.ToString(mode.ToCultureInfo());

        public static string ToCultureString(this double number, CultureInfoMode mode) =>
            number.ToString(mode.ToCultureInfo());

        public static string ToCultureString(this float number, CultureInfoMode mode) =>
            number.ToString(mode.ToCultureInfo());
    }
}
