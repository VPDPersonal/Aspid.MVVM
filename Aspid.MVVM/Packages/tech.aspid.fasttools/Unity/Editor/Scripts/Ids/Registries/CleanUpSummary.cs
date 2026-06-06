using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Ids.Editors
{
    internal readonly struct CleanUpSummary
    {
        public readonly int EmptyCount;
        public readonly int DuplicateCount;

        public CleanUpSummary(int emptyCount, int duplicateCount)
        {
            EmptyCount = emptyCount;
            DuplicateCount = duplicateCount;
        }

        public int Total => 
            EmptyCount + DuplicateCount;

        public string ToShortLabel()
        {
            var parts = new List<string>();
            if (DuplicateCount > 0) parts.Add($"{DuplicateCount} duplicates");
            if (EmptyCount > 0) parts.Add($"{EmptyCount} empty name" + (EmptyCount is 1 ? string.Empty : "s"));
            
            return $"⚠ {Total} invalid enter{(Total == 1 ? "y" : "ies")} ({string.Join(", ", parts)})";
        }
    }
}
