using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.SerializeReferences.Editors
{
    /// <summary>
    /// Per-asset memo over <see cref="SerializeReferenceHelpers.BuildConstraintMap"/>: the declared field type backing
    /// each <c>(fileId, rid)</c>, so the many lookups a repair surface makes cost one scan per distinct asset.
    /// </summary>
    /// <remarks>
    /// Building one map is a <c>LoadAllAssetsAtPath</c> plus a full <c>SerializedObject</c> walk, so every picker open
    /// must not re-scan. The flip side is staleness: <see cref="Clear"/> after any edit that rewrote the YAML, or the
    /// next lookup answers from the pre-edit file.
    /// </remarks>
    internal sealed class SerializeReferenceConstraintCache
    {
        private readonly Dictionary<string, Dictionary<(long fileId, long rid), Type>> _maps = new(StringComparer.Ordinal);

        /// <summary>
        /// The declared field type backing <paramref name="rid"/>, or <see langword="null"/> (unconstrained) for an
        /// orphaned payload or an unresolvable field type.
        /// </summary>
        /// <remarks>Keyed by exact <c>(fileId, rid)</c> since rids collide across documents.</remarks>
        public Type Resolve(string assetPath, long fileId, long rid)
        {
            if (!_maps.TryGetValue(assetPath, out var map))
            {
                map = SerializeReferenceHelpers.BuildConstraintMap(assetPath);
                _maps[assetPath] = map;
            }

            return map.GetValueOrDefault((fileId, rid));
        }

        /// <summary>Drops every memoised map so the next lookup re-reads the (possibly rewritten) files.</summary>
        public void Clear() => _maps.Clear();
    }
}
