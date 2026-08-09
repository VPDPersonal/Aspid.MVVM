using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.SerializeReferences.Editors
{
    /// <summary>
    /// The set of managed references that became missing since the last scan, grouped count metadata included.
    /// </summary>
    internal readonly struct BreakageReport
    {
        public readonly IReadOnlyList<BreakageEntry> Entries;
        public readonly int TypeCount;

        public BreakageReport(IReadOnlyList<BreakageEntry> entries, int typeCount)
        {
            Entries = entries;
            TypeCount = typeCount;
        }

        public bool HasAny => Entries is { Count: > 0 };
    }
}
