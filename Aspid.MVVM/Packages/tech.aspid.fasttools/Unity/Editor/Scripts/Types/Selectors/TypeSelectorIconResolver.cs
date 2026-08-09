using System;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Types.Editors
{
    /// <summary>
    /// Resolves the <see cref="TypeSelectorDisplayAttribute.Icon"/> string to a <see cref="Texture"/>:
    /// a project-relative asset path (<c>Assets/…</c>, <c>Packages/…</c>), a <c>Resources</c> texture
    /// path, or a built-in editor icon name.
    /// </summary>
    /// <remarks>
    /// Successful lookups are cached for the lifetime of the domain to keep row binding cheap; misses are
    /// not cached, so an icon whose asset is imported or renamed later is picked up on the next bind.
    /// </remarks>
    internal static class TypeSelectorIconResolver
    {
        private static readonly Dictionary<string, Texture> _cache = new();

        internal static Texture Resolve(string icon)
        {
            if (string.IsNullOrWhiteSpace(icon)) return null;

            if (_cache.TryGetValue(icon, out var cached))
            {
                // Unity-lifetime check, not a C# null check: a cached texture can be DESTROYED later (asset deleted,
                // Resources unloaded on play-mode load) — serving it binds an invisible icon forever. Drop the entry
                // and fall through to a reload (or an uncached retry on the next bind).
                if (cached) return cached;
                _cache.Remove(icon);
            }

            var texture = LoadIcon(icon);

            // Only cache hits: a miss may be a not-yet-imported / freshly-renamed asset, so leave it uncached and
            // retry on the next bind instead of pinning a null for the whole domain lifetime.
            if (texture is not null)
                _cache[icon] = texture;

            return texture;
        }

        private static Texture LoadIcon(string icon)
        {
            // A project-relative asset path (e.g. "Assets/Art/Icons/MyIcon.png") is loaded straight through the
            // AssetDatabase, so the icon can live anywhere in the project — not only inside a Resources folder. The path
            // must carry its file extension, exactly as the AssetDatabase expects.
            if (icon.StartsWith("Assets/", StringComparison.Ordinal) ||
                icon.StartsWith("Packages/", StringComparison.Ordinal))
                return AssetDatabase.LoadAssetAtPath<Texture>(icon);

            // A slash signals a Resources path (e.g. "Icons/MyIcon") rather than a built-in editor icon name. Probing
            // such a string through IconContent first logs a "Unable to load icon" warning to the console on every
            // miss, so for path-shaped strings the Resources load is tried first and IconContent is only the fallback.
            if (icon.Contains('/'))
            {
                var resource = Resources.Load<Texture>(icon);
                if (resource is not null) return resource;

                var pathContent = EditorGUIUtility.IconContent(icon);
                return pathContent?.image;
            }

            // Built-in editor icon (e.g. "d_ScriptableObject Icon"). IconContent never throws but may
            // return an empty content whose image is null.
            var content = EditorGUIUtility.IconContent(icon);
            return content?.image ?? Resources.Load<Texture>(icon);
        }
    }
}
