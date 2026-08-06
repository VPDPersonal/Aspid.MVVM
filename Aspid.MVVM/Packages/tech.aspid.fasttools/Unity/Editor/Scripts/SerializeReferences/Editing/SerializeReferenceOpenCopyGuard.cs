using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.SerializeReferences.Editors
{
    /// <summary>
    /// The writability test every YAML rewrite applies first: an asset loaded as a scene or open in Prefab Mode keeps
    /// an in-memory copy that wins on its next save, so a file edit under it would be silently clobbered.
    /// </summary>
    /// <remarks>
    /// Single-asset callers use <see cref="BlockedByOpenCopy"/>, which reports the refusal through a dialog; bulk
    /// callers use <see cref="IsWritable(string, string)"/> with a hoisted stage path so a batch resolves the open
    /// Prefab Mode stage once instead of once per entry.
    /// </remarks>
    internal static class SerializeReferenceOpenCopyGuard
    {
        /// <summary>The open Prefab Mode stage's asset path, or <see langword="null"/> when no stage is open.</summary>
        public static string CurrentPrefabStagePath() => PrefabStageUtility.GetCurrentPrefabStage()?.assetPath;

        /// <summary>Whether <paramref name="assetPath"/> can be rewritten on disk right now.</summary>
        public static bool IsWritable(string assetPath) => IsWritable(assetPath, CurrentPrefabStagePath());

        /// <inheritdoc cref="IsWritable(string)"/>
        /// <param name="assetPath">The asset to test.</param>
        /// <param name="prefabStagePath">A pre-resolved <see cref="CurrentPrefabStagePath"/>, hoisted out of a batch loop.</param>
        public static bool IsWritable(string assetPath, string prefabStagePath) =>
            !IsOpenInScene(assetPath) && !IsOpenInPrefabMode(assetPath, prefabStagePath);

        /// <summary>
        /// Single-asset guard: returns <see langword="true"/> — and explains why through a dialog — when the edit must
        /// be abandoned because an open copy would overwrite it.
        /// </summary>
        public static bool BlockedByOpenCopy(string assetPath)
        {
            var openInPrefabMode = IsOpenInPrefabMode(assetPath, CurrentPrefabStagePath());
            if (!IsOpenInScene(assetPath) && !openInPrefabMode) return false;

            EditorUtility.DisplayDialog(
                "Asset References",
                "This asset is open " + (openInPrefabMode ? "in Prefab Mode" : "as a loaded scene") +
                " — a file rewrite would be overwritten by its next save.\n\n" +
                "Close it and rescan, or repair the field directly in the Inspector.",
                "OK");
            return true;
        }

        private static bool IsOpenInScene(string assetPath) => SceneManager.GetSceneByPath(assetPath).isLoaded;

        private static bool IsOpenInPrefabMode(string assetPath, string prefabStagePath) =>
            !string.IsNullOrEmpty(prefabStagePath) &&
            string.Equals(prefabStagePath, assetPath, System.StringComparison.Ordinal);
    }
}
