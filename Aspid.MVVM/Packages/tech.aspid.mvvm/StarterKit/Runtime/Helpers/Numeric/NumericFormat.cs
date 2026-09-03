// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Hands out the standard numeric format strings without building one per call.
    /// </summary>
    internal static class NumericFormat
    {
        private static readonly string[] _fixed = { "F0", "F1", "F2", "F3", "F4", "F5", "F6", "F7", "F8", "F9" };
        private static readonly string[] _grouped = { "N0", "N1", "N2", "N3", "N4", "N5", "N6", "N7", "N8", "N9" };

        /// <summary>
        /// Writes the fixed-point format for the specified number of decimals: <c>F2</c>.
        /// </summary>
        /// <param name="decimals">How many decimals to show. A negative count reads as none.</param>
        /// <returns>The format string.</returns>
        internal static string Fixed(int decimals) => Get(table: _fixed, specifier: 'F', decimals);

        /// <summary>
        /// Writes the grouped format for the specified number of decimals: <c>N2</c>.
        /// </summary>
        /// <param name="decimals">How many decimals to show. A negative count reads as none.</param>
        /// <returns>The format string.</returns>
        internal static string Grouped(int decimals) => Get(table: _grouped, specifier: 'N', decimals);

        private static string Get(string[] table, char specifier, int decimals) => decimals switch
        {
            < 0 => table[0],
            _ when decimals < table.Length => table[decimals],
            _ => specifier + decimals.ToString(),
        };
    }
}
