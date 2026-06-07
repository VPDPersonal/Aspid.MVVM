using Aspid.FastTools.Editors;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Ids.Editors
{
    internal static class IdStructDrawerHelper
    {
        public static string BuildCaption(IdStructDrawerContext ctx, out bool isMissing) =>
            BuildCaption(ctx.FindRegistry(), ctx.IntProperty.intValue, ctx.StringProperty.stringValue, out isMissing);

        public static string BuildCaption(IdRegistry registry, int id, string fallbackName, out bool isMissing)
        {
            var nameId = fallbackName ?? string.Empty;

            var hasName = registry is not null
                && id > 0
                && registry.TryGetName(id, out nameId);

            nameId ??= string.Empty;
            var hasNotNameId = string.IsNullOrEmpty(nameId);
            isMissing = registry is not null && id > 0 && !hasName;

            return isMissing
                ? hasNotNameId ? $"<Missing id {id}>" : $"<Missing '{nameId}'>"
                : hasNotNameId ? Constants.NoneOption : nameId;
        }
        
        public static void SyncStringFromInt(IdStructDrawerContext ctx)
        {
            var registry = ctx.FindRegistry();

            var currentId = ctx.IntProperty.intValue;
            if (currentId <= 0 || registry is null) return;

            if (!registry.TryGetName(currentId, out var registryName)) return;
            if (registryName == ctx.StringProperty.stringValue) return;

            ctx.StringProperty.SetStringAndApply(registryName);
            ctx.Property.ApplyModifiedProperties();
        }
        
        public static void ApplySelection(
            string selected,
            IdStructDrawerContext ctx)
        {
            var id = 0;
            var nameId = selected ?? string.Empty;
            
            if (!string.IsNullOrEmpty(nameId))
            {
                var registry = ctx.FindRegistry();
                if (registry is not null && registry.TryGetId(nameId, out var foundId))
                    id = foundId;
            }
            
            SetFields(id, nameId, ctx);
        }
        
        private static void SetFields(
            int id,
            string nameId,
            IdStructDrawerContext ctx)
        {
            ctx.IntProperty.SetIntAndApply(id);
            ctx.StringProperty.SetStringAndApply(nameId);
            ctx.Property.ApplyModifiedPropertiesWithoutUndo();
        }
    }
}
