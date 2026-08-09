using System;
using UnityEditor;
using UnityEngine;
using UnityEditor.Search;
using System.Collections.Generic;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.SerializeReferences.Editors
{
    /// <summary>
    /// Quick Search provider that lists every asset/field using a managed-reference type. Type <c>sr:Weapon</c> in the
    /// search window (or invoke "Find Usages" from a field's context menu) to get one result per use site, each pinging
    /// its asset. Backed by <see cref="SerializeReferenceTypeUsageIndex"/>, so it matches on the stored type identity —
    /// including types that no longer resolve.
    /// </summary>
    internal static class SerializeReferenceUsageSearchProvider
    {
        private const string ProviderId = "sr";
        private const string FilterId = "sr:";
        private const string DisplayName = "Managed References";

        [SearchItemProvider]
        public static SearchProvider CreateProvider() =>
            new(ProviderId, DisplayName)
            {
                filterId = FilterId,
                // Explicit-only: otherwise the provider joins every general search and the first keystroke would warm
                // a cold usage index (a modal full-project sweep). "sr:" queries and OpenSearch create explicit contexts.
                isExplicitProvider = true,
                priority = 9000,
                showDetailsOptions = ShowDetailsOptions.Description | ShowDetailsOptions.Preview,
                fetchItems = (context, items, provider) => FetchItems(context, items, provider),
                fetchThumbnail = (item, context) => AssetThumbnail(item),
                toObject = (item, type) => LoadAsset(item),
                trackSelection = (item, context) => Ping(item),
            };

        /// <summary>
        /// Opens the search window pre-seeded with the type's name under the <c>sr:</c> filter.
        /// </summary>
        public static void OpenSearch(Type type)
        {
            if (type is null) return;
            var query = $"{FilterId}{type.Name}";
            var context = SearchService.CreateContext(ProviderId, query);
            SearchService.ShowWindow(context, "Find Usages", saveFilters: false);
        }

        // Returns null — the synchronous fetch convention.
        private static object FetchItems(SearchContext context, List<SearchItem> items, SearchProvider provider)
        {
            var token = (context.searchQuery ?? string.Empty).Trim();
            if (token.StartsWith(FilterId, StringComparison.OrdinalIgnoreCase))
                token = token[FilterId.Length..].Trim();

            if (token.Length == 0) return null;

            foreach (var usage in SerializeReferenceTypeUsageIndex.AllUsages())
            {
                var className = usage.StoredType.Class ?? string.Empty;
                if (className.IndexOf(token, StringComparison.OrdinalIgnoreCase) < 0) continue;

                var path = AssetDatabase.GUIDToAssetPath(usage.Guid);
                if (string.IsNullOrEmpty(path)) continue;

                var id = $"{usage.Guid}:{usage.FileId}:{usage.Rid}";
                var missing = usage.Resolves ? string.Empty : "  (missing)";
                var label = $"{className}{missing}";
                var description = $"{path}  —  rid {usage.Rid}";

                var item = provider.CreateItem(context, id, label, description, null, path);
                items.Add(item);
            }

            return null;
        }

        private static Object LoadAsset(SearchItem item) =>
            item?.data is string path ? AssetDatabase.LoadAssetAtPath<Object>(path) : null;

        private static Texture2D AssetThumbnail(SearchItem item) =>
            item?.data is string path ? AssetDatabase.GetCachedIcon(path) as Texture2D : null;

        private static void Ping(SearchItem item)
        {
            var asset = LoadAsset(item);
            if (asset != null) EditorGUIUtility.PingObject(asset);
        }
    }
}
