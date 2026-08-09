using System;
using UnityEditor;
using System.Collections.Generic;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.SerializeReferences.Editors
{
    /// <summary>
    /// Names the owning object of a required-field violation for display — <c>"Component.field"</c>, or the field path
    /// alone when the owner cannot be identified. Shared by both References tabs so their required rows read alike.
    /// </summary>
    /// <remarks>
    /// A <see cref="GateViolation"/> carries only the asset path and file id, so the owner is resolved on demand by
    /// object-loading the asset and matching the id — the same lookup the gate scanner does internally to build the
    /// violation, just for display here. Best-effort: scenes cannot be object-loaded (see
    /// <see cref="SerializeReferenceHelpers.IsScene"/>), so a scene row shows the field path rather than guessing.
    /// Loads are memoised per asset path, since several violations commonly share one asset.
    /// </remarks>
    internal sealed class ViolationFieldLabels
    {
        private readonly Dictionary<string, Object[]> _assets = new(StringComparer.Ordinal);

        /// <summary>The violation's <c>"Component.field"</c> label, or its field path alone.</summary>
        public string Describe(GateViolation violation)
        {
            var component = ResolveComponentName(violation);
            return string.IsNullOrEmpty(component) ? violation.FieldPath : $"{component}.{violation.FieldPath}";
        }

        /// <summary>The violation's owning object type name, or an empty string when it cannot be identified.</summary>
        public string ResolveComponentName(GateViolation violation)
        {
            if (SerializeReferenceHelpers.IsScene(violation.AssetPath)) return string.Empty;

            if (!_assets.TryGetValue(violation.AssetPath, out var assets))
            {
                assets = AssetDatabase.LoadAllAssetsAtPath(violation.AssetPath);
                _assets[violation.AssetPath] = assets;
            }

            foreach (var asset in assets)
            {
                if (asset == null) continue;
                if (AssetDatabase.TryGetGUIDAndLocalFileIdentifier(asset, out _, out var fileId) && fileId == violation.FileId)
                    return asset.GetType().Name;
            }

            return string.Empty;
        }
    }
}
