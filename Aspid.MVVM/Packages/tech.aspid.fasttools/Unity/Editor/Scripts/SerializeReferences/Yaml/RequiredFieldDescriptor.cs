// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.SerializeReferences.Editors
{
    /// <summary>
    /// The serialized shape of a <c>[TypeSelector(Required = true)]</c> field, so the pure-YAML scene scan knows how to
    /// read its "unset" state without reflection.
    /// </summary>
    internal enum RequiredFieldKind
    {
        /// <summary>A <c>string</c> type-name field: unset == a null-or-empty scalar.</summary>
        String,

        /// <summary>A <c>[SerializeReference]</c> managed reference: unset == the null-id pointer.</summary>
        ManagedReference,

        /// <summary>A <see cref="Aspid.FastTools.Types.SerializableType"/> wrapper: unset == its nested
        /// <c>_assemblyQualifiedName</c> scalar is null-or-empty.</summary>
        SerializableType,
    }

    /// <summary>
    /// A serialized field that opts into the <c>[TypeSelector(Required = true)]</c> check, captured for the pure-YAML
    /// scene scan: the YAML field key, its <see cref="RequiredFieldKind"/>, and — for a field nested inside plain
    /// <c>[Serializable]</c> containers — the chain of container keys leading to it. Produced by reflection in
    /// <see cref="SerializeReferenceRequiredGate.GetRequiredFields"/> and consumed by
    /// <see cref="SerializeReferenceYamlEditor.FindUnsetRequiredFields"/>, which stays reflection-free.
    /// </summary>
    internal readonly struct RequiredFieldDescriptor
    {
        public readonly RequiredFieldKind Kind;
        public readonly string FieldName;

        /// <summary>Container keys from the document's top level down to <see cref="FieldName"/>'s parent;
        /// empty for a top-level field.</summary>
        public readonly string[] Parents;

        /// <summary>The dotted property path (<c>_loadout.primary</c>) — matches the shape
        /// <c>SerializedProperty.propertyPath</c> reports for the same field, so gate reports read alike.</summary>
        public string Path => Parents is { Length: > 0 } ? string.Join(".", Parents) + "." + FieldName : FieldName;

        public RequiredFieldDescriptor(string fieldName, RequiredFieldKind kind)
            : this(System.Array.Empty<string>(), fieldName, kind) { }

        public RequiredFieldDescriptor(string[] parents, string fieldName, RequiredFieldKind kind)
        {
            Kind = kind;
            Parents = parents;
            FieldName = fieldName;
        }
    }
}
