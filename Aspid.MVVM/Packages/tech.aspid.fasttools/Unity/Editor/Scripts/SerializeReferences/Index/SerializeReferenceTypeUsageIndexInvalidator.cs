using System;
using System.Linq;
using UnityEditor;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.SerializeReferences.Editors
{
    /// <summary>
    /// Keeps <see cref="SerializeReferenceTypeUsageIndex"/> incremental: patches a single asset's usages on import and
    /// coarsely resets on any candidate delete/move (a deleted path can no longer be resolved to a guid for a surgical
    /// strip). Mirrors the import-post-processor strategy of the Id system's cache invalidator.
    /// </summary>
    internal sealed class SerializeReferenceTypeUsageIndexInvalidator : AssetPostprocessor
    {
        // A change to the excluded-folder set must drop the warm index: exclusion is consulted only while the index is
        // (re)built, so a warm one would keep serving now-excluded assets. Reset is lazy and never warms a cold index.
        [InitializeOnLoadMethod]
        private static void HookSettings() =>
            SerializeReferenceSettings.ExcludedFoldersChanged += SerializeReferenceTypeUsageIndex.Reset;

        private static void OnPostprocessAllAssets(string[] imported, string[] deleted, string[] moved, string[] movedFrom)
        {
            // An in-place class rename reimports the .cs without touching any asset YAML, so a per-asset patch would
            // never run and the warm index would keep stale Resolves entries — only a coarse reset re-evaluates them.
            if (HasCandidate(deleted) || HasCandidate(moved) || HasScript(imported))
            {
                SerializeReferenceTypeUsageIndex.Reset();
                return;
            }

            foreach (var asset in imported)
            {
                if (SerializeReferenceHelpers.IsScanCandidate(asset))
                    SerializeReferenceTypeUsageIndex.RebuildAsset(asset);
            }
        }

        private static bool HasCandidate(string[] paths) =>
            paths.Any(SerializeReferenceHelpers.IsScanCandidate);

        private static bool HasScript(string[] paths) =>
            paths.Any(path => path.EndsWith(".cs", StringComparison.OrdinalIgnoreCase));
    }
}
