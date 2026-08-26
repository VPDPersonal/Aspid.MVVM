using System;
using Aspid.FastTools.Types;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Counts the items in a collection.
    /// </summary>
    /// <typeparam name="T">The type of the items.</typeparam>
    /// <remarks>A sequence carrying no count of its own is walked on every push.</remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Collection/To Number",
        Name = "Count",
        Tooltip = "Counts the items in a collection")]
    public class CollectionCountConverter<T> :
        IConverter<IEnumerable<T?>?, int>, IConverter<IEnumerable<T?>?, long>,
        IConverter<IEnumerable<T?>?, float>, IConverter<IEnumerable<T?>?, double>,
        IConverter<IReadOnlyCollection<T?>?, int>, IConverter<IReadOnlyCollection<T?>?, long>,
        IConverter<IReadOnlyCollection<T?>?, float>, IConverter<IReadOnlyCollection<T?>?, double>
    {
        /// <summary>
        /// Counts the specified collection.
        /// </summary>
        /// <param name="value">The collection to count.</param>
        /// <returns>The number of items, or zero when the collection is <see langword="null"/>.</returns>
        public int Convert(IReadOnlyCollection<T?>? value) => value?.Count ?? 0;

        #region Counted collection
        long IConverter<IReadOnlyCollection<T?>?, long>.Convert(IReadOnlyCollection<T?>? value) =>
            Convert(value);

        float IConverter<IReadOnlyCollection<T?>?, float>.Convert(IReadOnlyCollection<T?>? value) =>
            Convert(value);

        double IConverter<IReadOnlyCollection<T?>?, double>.Convert(IReadOnlyCollection<T?>? value) =>
            Convert(value);
        #endregion

        #region Any sequence
        int IConverter<IEnumerable<T?>?, int>.Convert(IEnumerable<T?>? value) =>
            Count(value);

        long IConverter<IEnumerable<T?>?, long>.Convert(IEnumerable<T?>? value) =>
            Count(value);

        float IConverter<IEnumerable<T?>?, float>.Convert(IEnumerable<T?>? value) =>
            Count(value);

        double IConverter<IEnumerable<T?>?, double>.Convert(IEnumerable<T?>? value) =>
            Count(value);
        #endregion

        private static int Count(IEnumerable<T?>? value)
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
