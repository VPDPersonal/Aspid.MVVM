using UnityEditor;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Ids.Editors
{
    internal sealed class IdRegistryResolverCacheInvalidator : AssetPostprocessor
    {
        private static void OnPostprocessAllAssets(string[] imported, string[] deleted, string[] moved, string[] movedFrom)
        {
            if (HasAssetPath(imported) || HasAssetPath(deleted) || HasAssetPath(moved))
                IdRegistryResolver.ClearCache();
        }

        private static bool HasAssetPath(string[] paths)
        {
            for (var i = 0; i < paths.Length; i++)
                if (paths[i].EndsWith(".asset", System.StringComparison.OrdinalIgnoreCase))
                    return true;
            return false;
        }
    }
}
