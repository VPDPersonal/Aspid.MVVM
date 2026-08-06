using System;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.SerializeReferences.Editors
{
    /// <summary>
    /// One managed reference that just became missing, plus its pre-computed best fix candidate (if any).
    /// </summary>
    internal readonly struct BreakageEntry
    {
        public readonly long Rid;
        public readonly long FileId;
        public readonly string AssetPath;
        public readonly ManagedTypeName StoredType;

        /// <summary>
        /// False for entries the per-asset repair flow cannot reach (currently scene-hosted references).
        /// </summary>
        public readonly bool IsRepairable;

        /// <summary>
        /// The top Smart-Fix suggestion (e.g. a declared <c>[MovedFrom]</c> rename), pre-ranked at detection time.
        /// </summary>
        public readonly SerializeReferenceRepairSuggestions.RepairCandidate? TopSuggestion;

        /// <summary>
        /// The authoritative <c>[MovedFrom]</c> rename target of the stored type (see
        /// <see cref="SerializeReferenceMovedFromResolver"/>), or <see langword="null"/>. Non-null means the
        /// reference is not really broken — Unity migrates it in memory at load; only the file is stale.
        /// </summary>
        public readonly Type MigrationTarget;

        public BreakageEntry(
            string assetPath,
            long fileId,
            long rid,
            ManagedTypeName storedType,
            bool isRepairable,
            SerializeReferenceRepairSuggestions.RepairCandidate? topSuggestion,
            Type migrationTarget)
        {
            Rid = rid;
            FileId = fileId;
            AssetPath = assetPath;
            StoredType = storedType;
            IsRepairable = isRepairable;
            TopSuggestion = topSuggestion;
            MigrationTarget = migrationTarget;
        }

        public string TypeName => StoredType.DisplayName;
    }
}
