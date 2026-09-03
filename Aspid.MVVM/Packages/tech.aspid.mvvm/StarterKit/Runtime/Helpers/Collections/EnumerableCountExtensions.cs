#nullable enable
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Counts a sequence without LINQ, reading the count off a collection when it has one.
    /// </summary>
    internal static class EnumerableCountExtensions
    {
        /// <summary>
        /// Counts the items in the specified sequence.
        /// </summary>
        /// <typeparam name="T">The type of the items.</typeparam>
        /// <param name="value">The sequence to count.</param>
        /// <returns>The number of items, or zero for <see langword="null"/>.</returns>
        internal static int CountItems<T>(this IEnumerable<T>? value)
        {
            switch (value)
            {
                case null: return 0;
                case IReadOnlyCollection<T> collection: return collection.Count;
                case ICollection<T> collection: return collection.Count;
            }

            var count = 0;

            foreach (var _ in value)
                count++;

            return count;
        }
    }
}
