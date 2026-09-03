using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Size limits of a <see cref="PrefabViewPool{T}"/>.
    /// </summary>
    public readonly struct PoolSettings
    {
        /// <summary>
        /// The maximum number of inactive views kept in the pool.
        /// </summary>
        public readonly int MaxCount;

        /// <summary>
        /// The number of views instantiated up front.
        /// </summary>
        public readonly int InitialCount;

        /// <param name="initialCount">The number of views instantiated up front.</param>
        /// <param name="maxCount">The maximum number of inactive views kept in the pool.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="initialCount"/> is negative or <paramref name="maxCount"/> is less than one.
        /// </exception>
        public PoolSettings(
            int initialCount,
            int maxCount = int.MaxValue)
        {
            if (initialCount < 0) throw new ArgumentOutOfRangeException(nameof(initialCount));
            if (maxCount < 1) throw new ArgumentOutOfRangeException(nameof(maxCount));

            MaxCount = maxCount;
            InitialCount = initialCount;
        }
    }
}
