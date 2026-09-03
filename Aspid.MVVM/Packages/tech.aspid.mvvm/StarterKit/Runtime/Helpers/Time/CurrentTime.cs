using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Picks the clock a moment is measured against.
    /// </summary>
    internal static class CurrentTime
    {
        /// <summary>
        /// Reads the current time on the clock matching the specified moment's kind.
        /// </summary>
        /// <param name="value">The moment being measured.</param>
        /// <param name="useUtc">The clock for an <see cref="DateTimeKind.Unspecified"/> moment: UTC or local.</param>
        /// <returns>The current time, UTC or local.</returns>
        internal static DateTime For(DateTime value, bool useUtc) => value.Kind switch
        {
            DateTimeKind.Utc => DateTime.UtcNow,
            DateTimeKind.Local => DateTime.Now,
            _ => useUtc ? DateTime.UtcNow : DateTime.Now
        };
    }
}
