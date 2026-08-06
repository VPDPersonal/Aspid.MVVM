using System.Linq;
using UnityEditor;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.SerializeReferences.Editors
{
    /// <summary>
    /// Coarsely clears <see cref="SerializeReferenceYamlProbeCache"/> whenever a managed-reference-bearing asset is
    /// imported, deleted or moved, so the per-property probe never serves content from before an import. Mirrors the
    /// import-post-processor strategy of the Id system's cache invalidator.
    /// </summary>
    internal sealed class SerializeReferenceYamlProbeCacheInvalidator : AssetPostprocessor
    {
        private static void OnPostprocessAllAssets(string[] imported, string[] deleted, string[] moved, string[] movedFrom)
        {
            if (HasCandidate(imported) || HasCandidate(deleted) || HasCandidate(moved))
                SerializeReferenceYamlProbeCache.ClearCache();
        }

        private static bool HasCandidate(string[] paths) =>
            paths.Any(SerializeReferenceYaml.IsCandidateAssetPath);
    }
}
