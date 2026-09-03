using System.Globalization;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Provides extension methods for <see cref="CultureInfoMode"/>.
    /// </summary>
    public static class ToCultureStringExtensions
    {
        /// <summary>
        /// Resolves the culture a <see cref="CultureInfoMode"/> names.
        /// </summary>
        /// <param name="mode">The mode to resolve.</param>
        /// <returns>
        /// The named culture; an undeclared mode is reported and reads as the current culture.
        /// </returns>
        /// <remarks>
        /// Both <see cref="CultureInfo.DefaultThreadCurrentCulture"/> and
        /// <see cref="CultureInfo.DefaultThreadCurrentUICulture"/> are <see langword="null"/> until an
        /// application sets them, so those modes read as the corresponding current culture.
        /// </remarks>
        public static CultureInfo ToCultureInfo(this CultureInfoMode mode) => mode switch
        {
            CultureInfoMode.CurrentCulture => CultureInfo.CurrentCulture,
            CultureInfoMode.CurrentUICulture => CultureInfo.CurrentUICulture,
            CultureInfoMode.InvariantCulture => CultureInfo.InvariantCulture,
            CultureInfoMode.InstalledUICulture => CultureInfo.InstalledUICulture,
            CultureInfoMode.DefaultThreadCurrentCulture => CultureInfo.DefaultThreadCurrentCulture ?? CultureInfo.CurrentCulture,
            CultureInfoMode.DefaultThreadCurrentUICulture => CultureInfo.DefaultThreadCurrentUICulture ?? CultureInfo.CurrentUICulture,
            _ => Undeclared(mode)
        };

        /// <summary>
        /// Writes the specified number in the culture the mode names.
        /// </summary>
        /// <param name="number">The number to write.</param>
        /// <param name="mode">The culture to write it in.</param>
        /// <returns>The number as text.</returns>
        public static string ToCultureString(this int number, CultureInfoMode mode) =>
            number.ToString(provider: mode.ToCultureInfo());

        /// <inheritdoc cref="ToCultureString(int, CultureInfoMode)"/>
        public static string ToCultureString(this uint number, CultureInfoMode mode) =>
            number.ToString(provider: mode.ToCultureInfo());

        /// <inheritdoc cref="ToCultureString(int, CultureInfoMode)"/>
        public static string ToCultureString(this long number, CultureInfoMode mode) =>
            number.ToString(provider: mode.ToCultureInfo());

        /// <inheritdoc cref="ToCultureString(int, CultureInfoMode)"/>
        public static string ToCultureString(this double number, CultureInfoMode mode) =>
            number.ToString(provider: mode.ToCultureInfo());

        /// <inheritdoc cref="ToCultureString(int, CultureInfoMode)"/>
        public static string ToCultureString(this float number, CultureInfoMode mode) =>
            number.ToString(provider: mode.ToCultureInfo());

        /// <inheritdoc cref="ToCultureString(int, CultureInfoMode)"/>
        public static string ToCultureString(this decimal number, CultureInfoMode mode) =>
            number.ToString(provider: mode.ToCultureInfo());

        private static CultureInfo Undeclared(CultureInfoMode mode)
        {
            ConverterLogger.LogError(typeof(CultureInfoMode),
                problem: $"{mode.Describe()} is not a declared value",
                consequence: "Using the current culture.");

            return CultureInfo.CurrentCulture;
        }
    }
}
