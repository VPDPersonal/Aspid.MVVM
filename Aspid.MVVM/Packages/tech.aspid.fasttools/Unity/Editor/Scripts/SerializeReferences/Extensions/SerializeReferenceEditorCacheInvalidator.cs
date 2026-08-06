using System.Linq;
using UnityEditor;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.SerializeReferences.Editors
{
    /// <summary>
    /// Editor-assembly companion to <see cref="SerializeReferenceYamlProbeCacheInvalidator"/> (which lives in the
    /// dependency-free Yaml assembly and cannot reach these caches). rid and file id are stable across VCS
    /// operations, so after an external change — a <c>git checkout</c>, an outside tool rewriting an asset — a cached
    /// Smart-Fix ranking or the multi-select all-missing memo could describe data that no longer exists; the surfaced
    /// one-click fix would then re-point a reference against a stale identity. Both are dropped whenever a
    /// managed-reference-bearing asset is imported, deleted or moved.
    /// </summary>
    internal sealed class SerializeReferenceEditorCacheInvalidator : AssetPostprocessor
    {
        private static void OnPostprocessAllAssets(string[] imported, string[] deleted, string[] moved, string[] movedFrom)
        {
            if (!HasCandidate(imported) && !HasCandidate(deleted) && !HasCandidate(moved)) return;

            SerializeReferenceRepairSuggestions.ClearCache();
            SerializeReferenceHelpers.InvalidateMixedTypesCache();
            SerializeReferenceHelpers.InvalidateMissingTypeMemo();
        }

        private static bool HasCandidate(string[] paths) =>
            paths.Any(SerializeReferenceYaml.IsCandidateAssetPath);
    }
}
