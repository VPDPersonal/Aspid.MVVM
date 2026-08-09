// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.SerializeReferences.Editors
{
    /// <summary>
    /// One unset required field found by the pure-YAML scene scan: the owning object document (<see cref="FileId"/>),
    /// the field's YAML key and — for a managed reference — the null id it read (<c>-2</c>); <c>0</c> for a string field.
    /// </summary>
    internal readonly struct RequiredViolationEntry
    {
        public readonly long Rid;
        public readonly long FileId;
        public readonly string FieldName;

        public RequiredViolationEntry(long fileId, string fieldName, long rid)
        {
            Rid = rid;
            FileId = fileId;
            FieldName = fieldName;
        }
    }
}
