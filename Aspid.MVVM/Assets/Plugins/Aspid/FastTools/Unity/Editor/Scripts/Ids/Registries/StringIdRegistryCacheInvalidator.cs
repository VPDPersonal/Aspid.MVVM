using UnityEditor;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Ids.Editors
{
    internal sealed class StringIdRegistryCacheInvalidator : AssetPostprocessor
    {
        private static void OnPostprocessAllAssets(string[] imported, string[] deleted, string[] moved, string[] movedFrom)
        {
            if (imported.Length > 0 || deleted.Length > 0 || moved.Length > 0)
                StringIdRegistryHelper.ClearCache();
        }
    }
}
