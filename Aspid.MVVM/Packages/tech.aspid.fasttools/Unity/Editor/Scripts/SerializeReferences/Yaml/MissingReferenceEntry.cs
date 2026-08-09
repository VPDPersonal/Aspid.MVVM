// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.SerializeReferences.Editors
{
    /// <summary>
    /// A single orphaned managed-reference entry found in an asset's YAML: the document it lives in
    /// (<see cref="FileId"/>), its <c>RefIds</c> id and the stored (unresolvable) type. Surfaced by the
    /// asset-level repair tool, which finds every such entry regardless of nesting depth or child object.
    /// </summary>
    internal readonly struct MissingReferenceEntry
    {
        public readonly long Rid;
        public readonly long FileId;
        public readonly ManagedTypeName StoredType;

        public MissingReferenceEntry(long fileId, long rid, ManagedTypeName storedType)
        {
            Rid = rid;
            FileId = fileId;
            StoredType = storedType;
        }
    }
}
