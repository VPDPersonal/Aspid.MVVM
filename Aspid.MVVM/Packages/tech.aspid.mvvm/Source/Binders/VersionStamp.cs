// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Marks a revision of an operation so earlier work can detect when it is stale.
    /// </summary>
    /// <remarks>Use the default value as the initial stamp and compare stamps from the same sequence.</remarks>
    public readonly struct VersionStamp
    {
        private readonly uint _value;

        private VersionStamp(uint value) =>
            _value = value;

        /// <summary>
        /// Returns the next stamp, wrapping back to the default value after the last revision.
        /// </summary>
        /// <returns>The next stamp in the sequence.</returns>
        public VersionStamp Next() =>
            new(unchecked(_value + 1));

        /// <summary>
        /// Checks whether two stamps mark the same revision.
        /// </summary>
        /// <param name="other">The stamp from the same sequence to compare.</param>
        /// <returns><see langword="true"/> if the revisions match, otherwise <see langword="false"/>.</returns>
        public bool Matches(VersionStamp other) =>
            _value == other._value;
    }
}
