// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.SerializeReferences.Editors
{
    /// <summary>
    /// What a gate violation is: a missing managed-reference type, or an unset required reference.
    /// </summary>
    internal enum GateViolationKind
    {
        MissingType,
        RequiredUnset,
    }
}
