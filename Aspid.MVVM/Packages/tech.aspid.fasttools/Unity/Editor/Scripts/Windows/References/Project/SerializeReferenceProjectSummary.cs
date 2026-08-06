using System.Text;
using System.Collections.Generic;
using static Aspid.FastTools.SerializeReferences.Editors.SerializeReferenceAuditUI;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.SerializeReferences.Editors
{
    /// <summary>
    /// The Project References audit's copy: the results headline and hint, each group card's count line and band
    /// label, and the capped before/after previews the bulk confirmations show. Pure string composition.
    /// </summary>
    internal static class SerializeReferenceProjectSummary
    {
        // Beyond this a confirmation dialog stops being readable, so the rest is reported as a remainder line.
        private const int MaxPreviewedEntries = 8;

        /// <summary>
        /// The results headline. Only non-zero parts make it, and <paramref name="brokenCount"/> excludes the pending
        /// migrations — a <c>[MovedFrom]</c> rename with a one-click fix shouldn't inflate the alarm number.
        /// </summary>
        public static string BuildResultsHeaderText(int brokenCount, int migrationCount, int requiredCount)
        {
            var parts = new List<string>(3);
            if (brokenCount > 0) parts.Add(BuildCountText(brokenCount, "missing reference"));
            if (migrationCount > 0) parts.Add(BuildCountText(migrationCount, "pending migration"));
            if (requiredCount > 0) parts.Add(BuildCountText(requiredCount, "required violation"));

            return string.Join(", ", parts);
        }

        /// <summary>The dim line under the headline, naming what a card's affordances do.</summary>
        public static string BuildResultsHintText(bool hasRequiredViolations)
        {
            const string hint = "Each group is a broken stored type — Fix all re-points its every entry to one replacement, or to <None>.";

            return hasRequiredViolations
                ? hint + " Click a required-violation row to jump to its asset."
                : hint;
        }

        /// <summary>A group card's "N entries · M files" line.</summary>
        public static string BuildGroupCountText(MissingReferenceGroup group)
        {
            var entries = group.Entries.Count;
            var files = group.FileCount;
            var entryText = entries == 1 ? "1 entry" : $"{entries} entries";
            var fileText = files == 1 ? "1 file" : $"{files} files";
            return $"{entryText} · {fileText}";
        }

        /// <summary>
        /// A group card's band label: the verb plus a trailing chevron the picker host swaps in place.
        /// </summary>
        /// <remarks>
        /// A broken group's picker fixes ("Fix all"); on a migration card nothing is broken and the picker is the
        /// manual escape hatch beside the one-click <c>Migrate all</c> row, so its verb is "Reassign all".
        /// </remarks>
        public static string BuildFixAllLabel(MissingReferenceGroup group, bool isMigration) =>
            $"{(isMigration ? "Reassign all" : "Fix all")} ({group.Entries.Count})  ▼";

        /// <summary>
        /// The old → new preview of the YAML a bulk fix will rewrite, using the same <c>TryComputeRewrite</c> the
        /// rewrite applies, so the preview is exactly what gets written.
        /// </summary>
        public static string BuildDiffPreview(IReadOnlyList<MissingReferenceLocation> entries, ManagedTypeName newType)
        {
            var builder = new StringBuilder();
            builder.AppendLine("Changes:");

            // Compute first, render second: an uncomputable entry must neither vanish silently nor inflate the
            // "…and N more" remainder.
            var edits = new List<(MissingReferenceLocation entry, RewriteEdit edit)>(entries.Count);
            foreach (var entry in entries)
            {
                if (SerializeReferenceYamlEditor.TryComputeRewrite(entry.AssetPath, entry.Entry.FileId, entry.Entry.Rid, newType, out var edit))
                    edits.Add((entry, edit));
            }

            for (var i = 0; i < edits.Count && i < MaxPreviewedEntries; i++)
            {
                var (entry, edit) = edits[i];
                builder.AppendLine($"  {System.IO.Path.GetFileName(entry.AssetPath)} (rid {entry.Entry.Rid}):");
                builder.AppendLine($"    - {edit.OldLine.Trim()}");
                builder.AppendLine($"    + {edit.NewLine.Trim()}");
            }

            if (edits.Count > MaxPreviewedEntries)
                builder.AppendLine($"  …and {edits.Count - MaxPreviewedEntries} more");

            var uncomputable = entries.Count - edits.Count;
            if (uncomputable > 0)
                builder.AppendLine($"  ({uncomputable} entr{(uncomputable == 1 ? "y" : "ies")} could not be previewed)");

            builder.AppendLine();
            return builder.ToString();
        }

        /// <summary>The capped file + rid list for a clear confirmation. No before/after lines — the whole entry is being dropped.</summary>
        public static string BuildClearPreview(IReadOnlyList<MissingReferenceLocation> entries)
        {
            var builder = new StringBuilder();
            builder.AppendLine("Clears:");

            var shown = 0;
            foreach (var entry in entries)
            {
                if (shown >= MaxPreviewedEntries)
                {
                    builder.AppendLine($"  …and {entries.Count - shown} more");
                    break;
                }

                builder.AppendLine($"  {System.IO.Path.GetFileName(entry.AssetPath)} (rid {entry.Entry.Rid})");
                shown++;
            }

            builder.AppendLine();
            return builder.ToString();
        }
    }
}
