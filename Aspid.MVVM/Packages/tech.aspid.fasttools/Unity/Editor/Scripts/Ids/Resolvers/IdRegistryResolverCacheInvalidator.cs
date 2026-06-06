using System;
using System.Linq;
using UnityEditor;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Ids.Editors
{
    internal sealed class IdRegistryResolverCacheInvalidator : AssetPostprocessor
    {
        private const string FileType = ".asset";
        
        private static void OnPostprocessAllAssets(string[] imported, string[] deleted, string[] moved, string[] movedFrom)
        {
            if (HasAssetPath(deleted) || HasAssetPath(moved))
            {
                IdRegistryResolver.ClearCache();
                UniqueIdIndex.Reset();
                return;
            }

            foreach (var asset in imported)
            {
                if (!asset.EndsWith(FileType, StringComparison.OrdinalIgnoreCase)) continue;
                
                IdRegistryResolver.OnAssetImported(asset);
                UniqueIdIndex.OnAssetChanged(asset);
            }
        }

        private static bool HasAssetPath(string[] paths) =>
            paths.Any(path => path.EndsWith(FileType, StringComparison.OrdinalIgnoreCase));
    }
}
