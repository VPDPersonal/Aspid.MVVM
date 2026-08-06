// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.SerializeReferences.Editors
{
    /// <summary>
    /// One gate violation located during a project scan.
    /// </summary>
    internal readonly struct GateViolation
    {
        public readonly long Rid;
        public readonly long FileId;
        public readonly string AssetPath;
        public readonly string FieldPath;
        public readonly GateViolationKind Kind;
        public readonly ManagedTypeName StoredType;

        public GateViolation(
            string assetPath,
            long fileId,
            long rid,
            ManagedTypeName storedType,
            GateViolationKind kind,
            string fieldPath)
        {
            Rid = rid;
            Kind = kind;
            FileId = fileId;
            AssetPath = assetPath;
            StoredType = storedType;
            FieldPath = fieldPath;
        }

        public override string ToString()
        {
            var where = string.IsNullOrEmpty(FieldPath) ? $"rid {Rid}" : FieldPath;
            var what = Kind == GateViolationKind.MissingType ? $"missing type {StoredType.Class}" : "required value not set";

            return $"{AssetPath} : {where} -> {what}";
        }
    }
}
