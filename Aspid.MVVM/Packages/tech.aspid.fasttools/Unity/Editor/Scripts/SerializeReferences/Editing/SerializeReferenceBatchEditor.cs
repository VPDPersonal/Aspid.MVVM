using System;
using UnityEditor;
using System.Linq;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.SerializeReferences.Editors
{
    /// <summary>
    /// The bulk half of the repair tooling: rewriting or nulling many managed-reference entries at once, batched per
    /// file so each affected asset is reimported exactly once. Pure file work — the confirmations, receipts and the
    /// result rendering belong to the caller.
    /// </summary>
    /// <remarks>
    /// Every batch runs inside <see cref="AssetDatabase.StartAssetEditing"/>, which defers each
    /// <see cref="AssetDatabase.ImportAsset(string, ImportAssetOptions)"/> to one pass at the end, behind a
    /// cancel-free progress bar. Entries whose write fails are skipped, so the returned count is what actually
    /// changed on disk, not what was asked for.
    /// </remarks>
    internal static class SerializeReferenceBatchEditor
    {
        /// <summary>
        /// Splits entries into those safe to rewrite on disk and those open in Prefab Mode / a loaded scene, which
        /// must be repaired in memory instead.
        /// </summary>
        public static void SplitWritable(IReadOnlyList<MissingReferenceLocation> source,
            out List<MissingReferenceLocation> onDisk, out List<MissingReferenceLocation> inMemory)
        {
            var prefabStagePath = SerializeReferenceOpenCopyGuard.CurrentPrefabStagePath();
            onDisk = new List<MissingReferenceLocation>(source.Count);
            inMemory = new List<MissingReferenceLocation>();

            foreach (var entry in source)
            {
                if (SerializeReferenceOpenCopyGuard.IsWritable(entry.AssetPath, prefabStagePath)) onDisk.Add(entry);
                else inMemory.Add(entry);
            }
        }

        /// <summary>
        /// The entries safe to write, reporting through <paramref name="skipped"/> how many were held back because an
        /// open in-memory copy would clobber the file edit on its next save.
        /// </summary>
        public static List<MissingReferenceLocation> FilterWritable(IReadOnlyList<MissingReferenceLocation> source, out int skipped)
        {
            var prefabStagePath = SerializeReferenceOpenCopyGuard.CurrentPrefabStagePath();
            var writable = new List<MissingReferenceLocation>(source.Count);
            skipped = 0;

            foreach (var entry in source)
            {
                if (SerializeReferenceOpenCopyGuard.IsWritable(entry.AssetPath, prefabStagePath)) writable.Add(entry);
                else skipped++;
            }

            return writable;
        }

        /// <summary>
        /// The entries that still store <paramref name="appliedType"/>, i.e. the ones a receipt for that fix may
        /// safely revert; <paramref name="diverged"/> counts the rest.
        /// </summary>
        /// <remarks>
        /// A group can have been re-broken and fixed to a DIFFERENT type since the receipt was written, and blindly
        /// rewriting would destroy that newer fix. "Still holds it" is tested as a rewrite towards the applied type
        /// whose old line already equals its new line.
        /// </remarks>
        public static List<MissingReferenceLocation> FilterStillHolding(IReadOnlyList<MissingReferenceLocation> source,
            ManagedTypeName appliedType, out int diverged)
        {
            var holding = new List<MissingReferenceLocation>(source.Count);
            diverged = 0;

            foreach (var entry in source)
            {
                if (SerializeReferenceYamlEditor.TryComputeRewrite(entry.AssetPath, entry.Entry.FileId, entry.Entry.Rid, appliedType, out var edit) &&
                    edit.IsValid && string.Equals(edit.OldLine, edit.NewLine, StringComparison.Ordinal))
                    holding.Add(entry);
                else
                    diverged++;
            }

            return holding;
        }

        /// <summary>Rewrites every entry's stored type to <paramref name="targetType"/>; returns how many were written.</summary>
        public static int Rewrite(IReadOnlyList<MissingReferenceLocation> entries, ManagedTypeName targetType, string progressTitle) =>
            RunBatch(entries, progressTitle, (path, entry) =>
                SerializeReferenceYamlEditor.TryRewriteType(path, entry.Entry.FileId, entry.Entry.Rid, targetType));

        /// <summary>
        /// Nulls every entry to the null managed-reference id and drops its payload; returns how many were cleared.
        /// </summary>
        public static int Null(IReadOnlyList<MissingReferenceLocation> entries, string progressTitle) =>
            RunBatch(entries, progressTitle, (path, entry) =>
                SerializeReferenceYamlEditor.TryNullReference(path, entry.Entry.FileId, entry.Entry.Rid));

        /// <summary>
        /// Nulls each open entry on its live object — the file rewrite is skipped for open assets, so these stay in
        /// the audit until the asset is saved. Returns how many were cleared.
        /// </summary>
        public static int ClearOpenInMemory(IReadOnlyList<MissingReferenceLocation> entries, ManagedTypeName storedType)
        {
            var cleared = 0;
            foreach (var entry in entries)
            {
                if (SerializeReferenceHelpers.TryClearMissingReferenceInMemory(entry.AssetPath, entry.Entry.Rid, storedType))
                    cleared++;
            }

            return cleared;
        }

        /// <summary>How many distinct files <paramref name="entries"/> spans.</summary>
        public static int CountFiles(IEnumerable<MissingReferenceLocation> entries) =>
            entries.Select(entry => entry.AssetPath).Distinct(StringComparer.Ordinal).Count();

        // The shared per-file loop behind Rewrite / Null: only the per-entry edit differs. A file is reimported only
        // when at least one of its entries actually changed.
        private static int RunBatch(IReadOnlyList<MissingReferenceLocation> entries, string progressTitle,
            Func<string, MissingReferenceLocation, bool> edit)
        {
            var byFile = entries
                .GroupBy(entry => entry.AssetPath, StringComparer.Ordinal)
                .ToArray();

            var applied = 0;

            AssetDatabase.StartAssetEditing();
            try
            {
                for (var i = 0; i < byFile.Length; i++)
                {
                    var file = byFile[i];
                    EditorUtility.DisplayProgressBar(
                        progressTitle,
                        $"{file.Key}  ({i + 1}/{byFile.Length})",
                        (float)i / byFile.Length);

                    var changed = false;
                    foreach (var entry in file)
                    {
                        if (!edit(file.Key, entry)) continue;

                        applied++;
                        changed = true;
                    }

                    if (changed) AssetDatabase.ImportAsset(file.Key, ImportAssetOptions.ForceUpdate);
                }
            }
            finally
            {
                AssetDatabase.StopAssetEditing();
                EditorUtility.ClearProgressBar();
            }

            return applied;
        }
    }
}
